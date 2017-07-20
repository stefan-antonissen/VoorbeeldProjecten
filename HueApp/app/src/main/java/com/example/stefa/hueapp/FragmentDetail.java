package com.example.stefa.hueapp;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.SeekBar;
import android.widget.Switch;
import android.widget.TextView;

/**
 * Created by stefa on 11/20/2016.
 */

public class FragmentDetail extends Fragment {
    private int mId;
    private String mName;
    private String mType;
    private int mHue ;
    private int mBri;
    private int mSat;
    private boolean mPower;

    private SeekBar hueBar, satBar, briBar;
    private Switch powerSwitch;

    private Light mLight = new Light();

    private HttpHandler handler = new HttpHandler();

    @Override
    public void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);

        if (getArguments() != null) {
            mId = getArguments().getInt("id");
            mName = getArguments().getString("name");
            mType = getArguments().getString("type");
            mHue = getArguments().getInt("hue");
            mBri = getArguments().getInt("bri");
            mSat = getArguments().getInt("sat");
            mPower = getArguments().getBoolean("power");
            updateLight();

        }
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_detail, container, false);

        hueBar = (SeekBar) view.findViewById(R.id.hueBar);
        satBar = (SeekBar) view.findViewById(R.id.satBar);
        briBar = (SeekBar) view.findViewById(R.id.briBar);
        powerSwitch = (Switch) view.findViewById(R.id.switch1);

        hueBar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
            @Override
            public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                mLight.hue = progress;
                handler.setLightState(getContext(), mLight);
            }

            @Override
            public void onStartTrackingTouch(SeekBar seekBar) {

            }

            @Override
            public void onStopTrackingTouch(SeekBar seekBar) {

            }
        });

        satBar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
            @Override
            public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                mLight.sat = progress;
                handler.setLightState(getContext(), mLight);
            }

            @Override
            public void onStartTrackingTouch(SeekBar seekBar) {

            }

            @Override
            public void onStopTrackingTouch(SeekBar seekBar) {

            }
        });

        briBar.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {
            @Override
            public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                mLight.bri = progress;
                handler.setLightState(getContext(), mLight);
            }

            @Override
            public void onStartTrackingTouch(SeekBar seekBar) {

            }

            @Override
            public void onStopTrackingTouch(SeekBar seekBar) {

            }
        });

        powerSwitch.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mLight.power = !mLight.power;
                handler.setLightState(getContext(), mLight);
            }
        });

        hueBar.setProgress(mHue);
        satBar.setProgress(mSat);
        briBar.setProgress(mBri);
        powerSwitch.setChecked(mPower);
        return view;
    }

    public void updateLight() {
        if (mLight != null) {
            mLight.id = mId;
            mLight.name = mName;
            mLight.type = mType;
            mLight.hue = mHue;
            mLight.bri = mBri;
            mLight.sat = mSat;
            mLight.power = mPower;
        }
    }

    public void update(Bundle args) {
        mId = getArguments().getInt("id");
        mName = getArguments().getString("name");
        mType = getArguments().getString("type");
        mHue = getArguments().getInt("hue");
        mBri = getArguments().getInt("bri");
        mSat = getArguments().getInt("sat");
        mPower = getArguments().getBoolean("power");

        updateLight();

        hueBar.setProgress(mHue);
        satBar.setProgress(mSat);
        briBar.setProgress(mBri);
        powerSwitch.setChecked(mPower);
    }
}
