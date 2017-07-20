package com.example.stefanantonissen.contactcard;

import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.Context;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ListView;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.ArrayList;

public class MainActivity extends AppCompatActivity implements FragmentList.OnFragmentInteractionListener {

    ListView mPersonListView;
    PersonAdapter mPersonAdapter;
    ArrayList mPersonList = new ArrayList();
    HttpHandler handler;
    FileOutputStream outputStream;
    FileInputStream inputStream;
    String fileName = "contacts";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        handler = new HttpHandler();
        mPersonListView = (ListView) findViewById(R.id.contactListView) ;

        mPersonAdapter = new PersonAdapter(this, getLayoutInflater(), mPersonList);
        mPersonListView.setAdapter(mPersonAdapter);

        File file = new File(getFilesDir(), fileName);
        if (file.exists()) {
            try {
                inputStream = openFileInput(fileName);
                ObjectInputStream objectInputStream = new ObjectInputStream(inputStream);
                Person p;
                while ((p = (Person) objectInputStream.readObject()) != null) {
                    mPersonList.add(p);
                }
                objectInputStream.close();
                inputStream.close();
                mPersonAdapter.notifyDataSetChanged();
            } catch (Exception e) {
                e.printStackTrace();
            }

        }
    }

    @Override
    public void OnFragmentInteraction(Integer position, boolean newContact, boolean saveContacts, boolean deleteContacts) {
        if (position == null && saveContacts == false && deleteContacts == false) {
            handler.getRandomPerson(this, "https://randomuser.me/api", new ServerCallBack() {
                @Override
                public void onSuccess(Person person) {
                    mPersonList.add(person);
                    mPersonAdapter.notifyDataSetChanged();
                }
            });
        } else if (position == null && newContact == false && deleteContacts == false) {
            File file = new File(getFilesDir(), fileName);
            try {
                outputStream = openFileOutput(fileName, Context.MODE_PRIVATE);
                ObjectOutputStream objectOutputStream = new ObjectOutputStream(outputStream);
                for (Object p : mPersonList ) {
                    objectOutputStream.writeObject((Person) p);
                    Log.d("----", ((Person) p).toString());
                }
                objectOutputStream.close();
                outputStream.close();
            } catch (Exception e) {
                e.printStackTrace();
            }
        } else if (position == null && newContact == false && saveContacts == false) {
            deleteFile(fileName);
            mPersonList.clear();
            mPersonAdapter.notifyDataSetChanged();
        } else {
            FragmentDetail fDetail = (FragmentDetail) getFragmentManager().findFragmentById(R.id.fragment_detail);

            Person mPerson = (Person) mPersonList.get(position);
            Bundle args = new Bundle();
            args.putString("name", mPerson.name);
            args.putString("email", mPerson.email);
            args.putString("bitmap", mPerson.bitmap);
            args.putString("phone", mPerson.phone);
            args.putString("gender", mPerson.gender);

            if (fDetail != null) {
                fDetail.update(args);
            }
            else {
                FragmentDetail fragDetail = new FragmentDetail();
                fragDetail.setArguments(args);

                FragmentTransaction transaction  = getFragmentManager().beginTransaction();
                transaction.replace(R.id.fragment_container, fragDetail);
                transaction.addToBackStack(null);

                transaction.commit();
            }
        }
    }
}
