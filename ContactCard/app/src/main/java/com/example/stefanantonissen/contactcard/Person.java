package com.example.stefanantonissen.contactcard;

import android.graphics.Bitmap;
import android.os.Parcelable;

import java.io.Serializable;

/**
 * Created by Stefan Antonissen on 11/10/2016.
 */

public class Person implements Serializable {
    public String bitmap;
    public String gender;
    public String phone;
    public String name;
    public String email;
}
