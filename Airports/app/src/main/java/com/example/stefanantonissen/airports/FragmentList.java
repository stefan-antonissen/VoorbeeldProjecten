package com.example.stefanantonissen.airports;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.Spinner;

/**
 * Created by Stefan Antonissen on 11/10/2016.
 */

public class FragmentList extends Fragment implements AdapterView.OnItemClickListener {
    private OnFragmentInteractionListener mListener;

    public FragmentList() {

    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_list, container, false);
        ListView lv = (ListView) view.findViewById(R.id.airportListView);
        lv.setOnItemClickListener(this);
        Spinner sp = (Spinner) view.findViewById(R.id.spinner);
        sp.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                mListener.OnFragmentInteraction(position, null);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                mListener.OnFragmentInteraction(10, null);
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
        mListener.OnFragmentInteraction(null, position);
    }

    public interface OnFragmentInteractionListener {
        void OnFragmentInteraction(Integer pos, Integer position);
    }
}
