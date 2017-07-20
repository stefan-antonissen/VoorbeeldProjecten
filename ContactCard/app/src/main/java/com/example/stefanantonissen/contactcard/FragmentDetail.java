package com.example.stefanantonissen.contactcard;

import android.app.Fragment;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.media.Image;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import java.io.File;
import java.net.MalformedURLException;
import java.net.URL;

/**
 * Created by Stefan Antonissen on 11/10/2016.
 */

public class FragmentDetail extends Fragment {

    private Person mPerson;

    private static final String ARG_NAME = "name";
    private static final String ARG_GENDER = "gender";
    private static final String ARG_EMAIL = "email";
    private static final String ARG_BITMAP = "bitmap";
    private static final String ARG_PHONE = "phone";

    private String mName = "Voornaam Achternaam";
    private String mGender = "Male";
    private String mEmail = "email@example.com";
    private String mBitmap = "bm56";
    private String mPhone = "0612345678";

    private TextView nameView, genderView, emailView, phoneView;
    private ImageView imageView;

    private SharedPreferences settings;

    public static FragmentDetail newInstance(String name, String gender, String email, String bitmap, String phone) {
        FragmentDetail fragment = new FragmentDetail();
        Bundle args = new Bundle();
        args.putString(ARG_NAME, name);
        args.putString(ARG_GENDER, gender);
        args.putString(ARG_EMAIL, email);
        args.putString(ARG_BITMAP, bitmap);
        args.putString(ARG_PHONE, phone);
        fragment.setArguments(args);
        return fragment;
    }



    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mName = getArguments().getString(ARG_NAME);
            mGender = getArguments().getString(ARG_GENDER);
            mEmail = getArguments().getString(ARG_EMAIL);
            mBitmap = getArguments().getString(ARG_BITMAP);
            mPhone = getArguments().getString(ARG_PHONE);
        }
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_detail, container, false);
        nameView = (TextView) view.findViewById(R.id.nameText);
        nameView.setText(mName);
        genderView = (TextView) view.findViewById(R.id.genderText);
        genderView.setText(mGender);
        emailView = (TextView) view.findViewById(R.id.emailText);
        emailView.setText(mEmail);
        phoneView = (TextView) view.findViewById(R.id.phoneText);
        phoneView.setText(mPhone);
        int resourceId = view.getResources().getIdentifier(mBitmap, "drawable", "com.example.stefanantonissen.contactcard");
        imageView = (ImageView) view.findViewById(R.id.photoImageView);
        HttpHandler handler = new HttpHandler();
        handler.getImage(getContext(), mBitmap, new ImageCallBack() {
            @Override
            public void onSuccess(Bitmap bitmap) {
                imageView.setImageBitmap(bitmap);
            }
        });

        settings = this.getActivity().getSharedPreferences("fileName", 0);
        return view;
    }

    public void update(Bundle args) {
        mName = args.getString(ARG_NAME);
        mGender = args.getString(ARG_GENDER);
        mPhone = args.getString(ARG_PHONE);
        mEmail = args.getString(ARG_EMAIL);
        mBitmap = args.getString(ARG_BITMAP);
        nameView.setText(mName);
        genderView.setText(mGender);
        emailView.setText(mEmail);
        phoneView.setText(mPhone);
        int resourceId = getResources().getIdentifier(mBitmap, "drawable", "com.example.stefanantonissen.contactcard");
        Bitmap bitmap = BitmapFactory.decodeResource(getResources(), resourceId);
        imageView.setImageBitmap(bitmap);
    }

}
