package com.example.stefanantonissen.airports;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.readystatesoftware.sqliteasset.SQLiteAssetHelper;

/**
 * Created by Stefan Antonissen on 11/10/2016.
 */

public class AirportsDatabase extends SQLiteAssetHelper {
    private static final String DATABASE_NAME = "airports.sqlite";
    private static final int DATABASE_VERSION = 1;

    public AirportsDatabase(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }


    // Hier de CRUD methoden
    public Cursor getAirports() {
        SQLiteDatabase db = getReadableDatabase();

        String query = "SELECT * FROM airports WHERE iso_country = \"NL\"";
        Cursor c = db.rawQuery(query, null);
        c.moveToFirst();
        db.close();
        return c;
    }

    public Cursor getAirports(String country) {
        SQLiteDatabase db = getReadableDatabase();

        String query = "SELECT * FROM airports WHERE iso_country = \"" + country + "\"";
        Cursor c = db.rawQuery(query, null);
        c.moveToFirst();
        db.close();
        return c;
    }

    public Cursor getCountryList() {
        SQLiteDatabase db = getReadableDatabase();

        String query = "SELECT DISTINCT iso_country FROM airports ORDER BY iso_country ASC";
        Cursor c = db.rawQuery(query, null);
        c.moveToFirst();
        db.close();
        return c;
    }
}
