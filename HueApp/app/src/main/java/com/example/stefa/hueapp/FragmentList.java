package com.example.stefa.hueapp;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;

/**
 * Created by stefa on 11/20/2016.
 */

public class FragmentList extends Fragment implements AdapterView.OnItemClickListener {
    private OnFragmentInteractionListener mListener;

    public FragmentList() {

    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_list_view, container, false);
        ListView lv = (ListView) view.findViewById(R.id.lightListView);
        lv.setOnItemClickListener(this);
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
        mListener.OnFragmentInteraction(position);
    }

    public interface OnFragmentInteractionListener {
        public void OnFragmentInteraction(int position);
    }
}
