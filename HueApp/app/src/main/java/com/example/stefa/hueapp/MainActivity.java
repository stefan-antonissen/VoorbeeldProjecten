package com.example.stefa.hueapp;

import android.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity implements FragmentList.OnFragmentInteractionListener, ServerCallBack {

    private ListView lightListView;
    private ArrayList<Light> lightList;
    private ArrayAdapter lightAdapter;
    private HttpHandler handler;
    private ArrayList list;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        handler = new HttpHandler();

        lightListView = (ListView) findViewById(R.id.lightListView);

        lightList = new ArrayList<Light>();
        handler.getAllLights(this, "", this);

        list = new ArrayList<String>();
        for (Light l : lightList) {
            list.add(l.name);
        }

        lightAdapter = new ArrayAdapter<String>(getApplicationContext(), android.R.layout.simple_list_item_1, list);
        lightListView.setAdapter(lightAdapter);
    }

    @Override
    public void onSuccess(ArrayList<Light> lights) {
        lightList.clear();
        lightList.addAll(lights);
        list.clear();
        for (Light l : lightList) {
            list.add(l.name);
        }
        lightAdapter.notifyDataSetChanged();
    }

    @Override
    public void OnFragmentInteraction(int position) {
        FragmentDetail fragLight = (FragmentDetail) getFragmentManager().findFragmentById(R.id.fragment_detail);

        Light light = lightList.get(position);

        Bundle args = new Bundle();
        args.putString("name",  light.name);
        args.putString("type", light.type);
        args.putInt("bri", light.bri);
        args.putInt("hue", light.hue);
        args.putInt("sat", light.sat);
        args.putBoolean("power", light.power);
        args.putInt("id", light.id);
        if (fragLight != null) {
            fragLight.update(args);
        }
        else {
            FragmentDetail fragmentLight = new FragmentDetail();
            fragmentLight.setArguments(args);

            FragmentTransaction transaction  = getFragmentManager().beginTransaction();
            transaction.replace(R.id.fragment_container, fragmentLight);
            transaction.addToBackStack(null);

            transaction.commit();
        }

    }
}
