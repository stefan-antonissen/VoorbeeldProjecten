package com.example.stefanantonissen.contactcard;

import android.graphics.BitmapFactory;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import java.util.ArrayList;

public class PersonListView extends AppCompatActivity implements AdapterView.OnItemClickListener {

    ListView mPersonListView;
    PersonAdapter mPersonAdapter;
    ArrayList mPersonList = new ArrayList();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_list_view);

        mPersonListView = (ListView) findViewById(R.id.activityContactListView) ;

        mPersonAdapter = new PersonAdapter(this, getLayoutInflater(), mPersonList);
        mPersonListView.setAdapter(mPersonAdapter);



        mPersonListView.setOnItemClickListener(this);
    }

    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        Log.d("SelectedItemL ", position + "");
    }
}
