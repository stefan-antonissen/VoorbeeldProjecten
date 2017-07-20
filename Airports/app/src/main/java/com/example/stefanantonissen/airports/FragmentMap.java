package com.example.stefanantonissen.airports;

import android.app.Activity;
import android.app.Fragment;
import android.graphics.Color;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.TextView;

import com.google.android.gms.maps.CameraUpdate;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapFragment;
import com.google.android.gms.maps.MapView;
import com.google.android.gms.maps.MapsInitializer;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.LatLngBounds;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.PolygonOptions;

import org.w3c.dom.Text;

/**
 * Created by Stefan Antonissen on 11/10/2016.
 */

public class FragmentMap extends Fragment implements OnMapReadyCallback, GoogleMap.OnMapLoadedCallback {

    private static final String ARG_ICAO = "icao";
    private static final String ARG_NAME= "name";
    private static final String ARG_LONG = "longitude";
    private static final String ARG_LAT = "latitude";
    private static final String ARG_ELEV = "elevation";
    private static final String ARG_COUN = "country";
    private static final String ARG_MUNI = "municipality";

    private String mICAO;
    private String mName;
    private double mLong;
    private double mLat;
    private double mElev;
    private String mCountry;
    private String mMunicipality;
    private int mDistance;

    private TextView icaoView, nameView, longitudeView, latitudeView, elevationView, countryView, municipalityView, ehamView;
    private MapView mapFragment;
    private GoogleMap googleMap;

    static final LatLng EHAM = new LatLng(52.3086013794, 4.76388978958);
    private LatLng airportLocation = EHAM;
    private double earthRadius = 6371.0;

    private LatLngBounds bounds;

    private int calculateDistance(LatLng latLng) {
        double radLat = Math.toRadians(latLng.latitude - EHAM.latitude);
        double radLong = Math.toRadians(latLng.longitude - EHAM.longitude);
        double a = Math.sin(radLat / 2) * Math.sin(radLat / 2) + Math.cos(Math.toRadians(latLng.latitude)) * Math.cos(Math.toRadians(EHAM.latitude)) * Math.sin(radLong / 2) * Math.sin(radLong / 2);
        double c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        int distance = (int) Math.round(earthRadius * c);
        return distance;
    }

    public static FragmentMap newInstance(String icao, String name, long longitude, long latitude, double elevation, String country, String municipality) {
        FragmentMap fragment = new FragmentMap();
        Bundle args = new Bundle();
        args.putString(ARG_ICAO, icao);
        args.putString(ARG_NAME, name);
        args.putDouble(ARG_LONG, longitude);
        args.putDouble(ARG_LAT, latitude);
        args.putDouble(ARG_ELEV, elevation);
        args.putString(ARG_COUN, country);
        args.putString(ARG_MUNI, municipality);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);

        if (getArguments() != null) {
            mICAO = getArguments().getString(ARG_ICAO);
            mName = getArguments().getString(ARG_NAME);
            mLong = getArguments().getDouble(ARG_LONG);
            mLat = getArguments().getDouble(ARG_LAT);
            mElev = getArguments().getDouble(ARG_ELEV);
            mCountry = getArguments().getString(ARG_COUN);
            mMunicipality = getArguments().getString(ARG_MUNI);
            airportLocation = new LatLng(mLat, mLong);
            mDistance = calculateDistance(airportLocation);
        }
    }

    public void update(Bundle args) {
        mICAO = args.getString(ARG_ICAO);
        mName = args.getString(ARG_NAME);
        mLong = args.getDouble(ARG_LONG);
        mLat = args.getDouble(ARG_LAT);
        mElev = args.getDouble(ARG_ELEV);
        mCountry = args.getString(ARG_COUN);
        mMunicipality = args.getString(ARG_MUNI);
        airportLocation = new LatLng(mLat, mLong);
        mDistance = calculateDistance(airportLocation);


        icaoView.setText(mICAO);
        nameView.setText(mName);
        longitudeView.setText("" + mLong);
        latitudeView.setText("" + mLat);
        elevationView.setText("" + mElev);
        countryView.setText(mCountry);
        municipalityView.setText(mMunicipality);
        ehamView.setText("" + mDistance);

        googleMap.clear();
        mapFragment.getMapAsync((OnMapReadyCallback) this);
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_map, container, false);

        icaoView = (TextView) view.findViewById(R.id.icaoViewValue);
        nameView = (TextView) view.findViewById(R.id.nameViewValue);
        longitudeView = (TextView) view.findViewById(R.id.longitudeViewValue);
        latitudeView = (TextView) view.findViewById(R.id.latitudeViewValue);
        elevationView = (TextView) view.findViewById(R.id.elevationViewValue);
        countryView = (TextView) view.findViewById(R.id.countryViewValue);
        ehamView = (TextView) view.findViewById(R.id.ehamViewValue);
        municipalityView = (TextView) view.findViewById(R.id.municipalityViewValue);
        mapFragment = (MapView) view.findViewById(R.id.myMap);

        mapFragment.onCreate(savedInstanceState);
        if (mapFragment != null)
            mapFragment.getMapAsync((OnMapReadyCallback) this);

        icaoView.setText(mICAO);
        nameView.setText(mName);
        longitudeView.setText("" + mLong);
        latitudeView.setText("" + mLat);
        elevationView.setText("" + mElev);
        countryView.setText(mCountry);
        municipalityView.setText(mMunicipality);
        ehamView.setText("" + mDistance);
        return view;
    }

    @Override
    public void onMapLoaded() {
        CameraUpdate cameraUpdate = CameraUpdateFactory.newLatLngBounds(bounds, 13);
        this.googleMap.animateCamera(cameraUpdate);
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        this.googleMap = googleMap;

        this.googleMap.setOnMapLoadedCallback(this);

        this.googleMap.getUiSettings().setScrollGesturesEnabled(false);

        MapsInitializer.initialize(this.getActivity());

        LatLngBounds.Builder builder = new LatLngBounds.Builder();
        builder.include(airportLocation);
        builder.include(EHAM);
        bounds = builder.build();

        Marker maker = this.googleMap.addMarker(new MarkerOptions()
        .position(airportLocation)
        .title(mName)
        .snippet(mCountry));

        this.googleMap.setMapType(GoogleMap.MAP_TYPE_HYBRID);

        PolygonOptions polygonOptions = new PolygonOptions()
                .add(new LatLng(airportLocation.latitude, airportLocation.longitude),
                        new LatLng(EHAM.latitude, EHAM.longitude));
        polygonOptions.geodesic(true);

        polygonOptions.strokeColor(Color.RED);
        polygonOptions.strokeWidth(25);
        this.googleMap.addPolygon(polygonOptions);
    }

    @Override
    public void onResume() {
        super.onResume();
        if(mapFragment != null){
            mapFragment.onResume();
        }
    }

    @Override
    public void onPause() {
        if(mapFragment != null) {
            mapFragment.onPause();
        }
        super.onPause();
    }

    @Override
    public void onDestroy() {
        if (mapFragment != null) {
            try {
                mapFragment.onDestroy();
            } catch (NullPointerException e) {
                //Log.e("", "Error while attempting MapView.onDestroy(), ignoring exception", e);
            }
        }
        super.onDestroy();
    }

    @Override
    public void onLowMemory() {
        super.onLowMemory();
        if (mapFragment != null) {
            mapFragment.onLowMemory();
        }
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        if (mapFragment != null) {
            mapFragment.onSaveInstanceState(outState);
        }
    }
}
