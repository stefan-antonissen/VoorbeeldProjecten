package com.example.stefanantonissen.contactcard;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;

import java.util.ArrayList;

/**
 * Created by Stefan Antonissen on 11/10/2016.
 */

public class FragmentList extends Fragment implements AdapterView.OnItemClickListener {

    ListView mPersonListView;
    PersonAdapter mPersonAdapter;
    ArrayList mPersonList = new ArrayList();
    Button mAddButton, mSaveButton, mDeleteButton;

    private OnFragmentInteractionListener mListener;

    public FragmentList() {

    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, final ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_list_view, container, false);
        mPersonListView = (ListView) view.findViewById(R.id.contactListView);
        mPersonListView.setOnItemClickListener(this);
        mAddButton = (Button) view.findViewById(R.id.addContactButton);
        mAddButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mListener.OnFragmentInteraction(null, true, false, false);
            }
        });
        mSaveButton = (Button) view.findViewById(R.id.saveContactsButton);
        mSaveButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mListener.OnFragmentInteraction(null, false, true, false);
            }
        });
        mDeleteButton = (Button) view.findViewById(R.id.deleteContactsButton);
        mDeleteButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mListener.OnFragmentInteraction(null, false, false, true);
            }
        });

        return view;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);

        try {
            mListener = (OnFragmentInteractionListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString() + " must implement OnFragmentInteraction");
        }
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        Log.d("SelectedItemL ", position + "");
        /*Person mPerson = (Person) mPersonList.get(position);
        Bundle args = new Bundle();
        args.putString("name", mPerson.name);
        args.putInt("age", mPerson.age);
        args.putString("email", mPerson.email);
        args.putString("bitmap", mPerson.bitmap);*/
        mListener.OnFragmentInteraction(position, false, false, false);
    }

    public interface OnFragmentInteractionListener {
        public void OnFragmentInteraction(Integer args, boolean newContact, boolean saveContacts, boolean deleteContacts);
    }

}
