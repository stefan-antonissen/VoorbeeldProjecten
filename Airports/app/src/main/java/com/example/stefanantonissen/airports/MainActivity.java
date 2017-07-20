package com.example.stefanantonissen.airports;

import android.app.FragmentTransaction;
import android.database.Cursor;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.SpinnerAdapter;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity implements FragmentList.OnFragmentInteractionListener {

    private static final String ARG_ICAO = "icao";
    private static final String ARG_NAME= "name";
    private static final String ARG_LONG = "longitude";
    private static final String ARG_LAT = "latitude";
    private static final String ARG_ELEV = "elevation";
    private static final String ARG_COUN = "country";
    private static final String ARG_MUNI = "municipality";

    private ListView airportListView;
    private Spinner countrySpinnerView;
    private AirportCursorAdapter airportCursorAdapter;
    ArrayList list;
    ArrayList<Airport> airportList;
    ArrayList<String> countryList;
    ArrayAdapter<String> spinnerAdapter;
    ArrayAdapter<String> airportAdapter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        list = new ArrayList<String>();
        airportList = new ArrayList<Airport>();
        countryList = new ArrayList<String>();

        airportListView = (ListView) findViewById(R.id.airportListView);
        countrySpinnerView = (Spinner) findViewById(R.id.spinner);

        AirportsDatabase adb = new AirportsDatabase(this);
        Cursor cursor = adb.getAirports();

        cursor.moveToFirst();
        while( cursor.moveToNext() ) {
            String str = cursor.getString(cursor.getColumnIndex("name"));

            Airport airport = new Airport();
            airport.mICAO = cursor.getString(cursor.getColumnIndex("icao"));
            airport.mName = cursor.getString(cursor.getColumnIndex("name"));
            airport.mLong = cursor.getDouble(cursor.getColumnIndex("longitude"));
            airport.mLat = cursor.getDouble(cursor.getColumnIndex("latitude"));
            airport.mElev = cursor.getDouble(cursor.getColumnIndex("elevation"));
            airport.mCountry = cursor.getString(cursor.getColumnIndex("iso_country"));
            airport.mMunicipality = cursor.getString(cursor.getColumnIndex("municipality"));
            airportList.add(airport);

            list.add(str);
        }

        cursor = adb.getCountryList();

        cursor.moveToFirst();
        while (cursor.moveToNext()) {
            countryList.add(cursor.getString(cursor.getColumnIndex("iso_country")));
        }

        spinnerAdapter = new ArrayAdapter<String>(getApplicationContext(), android.R.layout.simple_spinner_dropdown_item, countryList);
        countrySpinnerView.setAdapter(spinnerAdapter);
        spinnerAdapter.notifyDataSetChanged();

        airportAdapter = new ArrayAdapter<String>(getApplicationContext(),
                android.R.layout.simple_list_item_1,
                list);
        airportListView.setAdapter(airportAdapter);

        airportAdapter.notifyDataSetChanged();
    }

    public void updateAirportList(String country) {
        list.clear();
        airportList.clear();
        AirportsDatabase adb = new AirportsDatabase(this);
        Cursor cursor = adb.getAirports(country);

        cursor.moveToFirst();
        while( cursor.moveToNext() ) {
            String str = cursor.getString(cursor.getColumnIndex("name"));

            Airport airport = new Airport();
            airport.mICAO = cursor.getString(cursor.getColumnIndex("icao"));
            airport.mName = cursor.getString(cursor.getColumnIndex("name"));
            airport.mLong = cursor.getDouble(cursor.getColumnIndex("longitude"));
            airport.mLat = cursor.getDouble(cursor.getColumnIndex("latitude"));
            airport.mElev = cursor.getDouble(cursor.getColumnIndex("elevation"));
            airport.mCountry = cursor.getString(cursor.getColumnIndex("iso_country"));
            airport.mMunicipality = cursor.getString(cursor.getColumnIndex("municipality"));
            airportList.add(airport);

            list.add(str);
        }
        airportAdapter.notifyDataSetChanged();
        spinnerAdapter.notifyDataSetChanged();
    }

    @Override
    public void OnFragmentInteraction(Integer pos, Integer position) {

        if (position == null)
        {
            updateAirportList(countryList.get(pos));
        } else {
            FragmentMap fMap = (FragmentMap) getFragmentManager().findFragmentById(R.id.fragment_map);

            Airport airport = airportList.get(position);

            Bundle args = new Bundle();
            args.putString(ARG_ICAO,  airport.mICAO);
            args.putString(ARG_NAME, airport.mName);
            args.putDouble(ARG_LONG, airport.mLong);
            args.putDouble(ARG_LAT, airport.mLat);
            args.putDouble(ARG_ELEV, airport.mElev);
            args.putString(ARG_COUN, airport.mCountry);
            args.putString(ARG_MUNI, airport.mMunicipality);
            if (fMap != null) {
                fMap.update(args);
            }
            else {
                FragmentMap fragMap = new FragmentMap();
                fragMap.setArguments(args);

                FragmentTransaction transaction  = getFragmentManager().beginTransaction();
                transaction.replace(R.id.fragment_container, fragMap);
                transaction.addToBackStack(null);

                transaction.commit();
            }
        }


    }
}
