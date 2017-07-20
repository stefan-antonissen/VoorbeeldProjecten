package com.example.stefanantonissen.contactcard;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.ImageRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONObject;

import java.net.URL;

/**
 * Created by Stefan Antonissen on 11/11/2016.
 */

public class HttpHandler {
    public Person person;
    public JSONObject json;

    public void getRandomPerson(Context context, String url, final ServerCallBack callback)
    {
        RequestQueue queue = Volley.newRequestQueue(context);
        //String url = "https://randomuser.me/api";
        StringRequest objectRequest = new StringRequest(Request.Method.GET, url,
                new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                person = new Person();
                try {
                    JSONObject obj = new JSONObject(response.toString());
                    JSONArray results = obj.getJSONArray("results");
                    String test = "";
                    JSONObject name = results.getJSONObject(0).getJSONObject("name");
                    String title = name.get("title").toString();
                    String first = name.get("first").toString();
                    String last = name.get("last").toString();
                    String email = results.getJSONObject(0).get("email").toString();
                    String picture = results.getJSONObject(0).getJSONObject("picture").get("medium").toString();
                    String gender = results.getJSONObject(0).get("gender").toString();
                    String phone = results.getJSONObject(0).get("phone").toString();
                    //String name = results.getJSONObject("name").get("first").toString();
                    //person.name = name.getString("title") + " " + name.getString("first") + " " + name.getString("last");
                    person.name = title + " " + first + " " + last;
                    person.bitmap = picture;
                    person.email = email;
                    person.phone = phone;
                    person.gender = gender;
                    callback.onSuccess(person);
                } catch (Exception e) {

                }
            }

            }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {

                }
            });
        queue.add(objectRequest);
    }

    public void getImage(Context context, String url, final ImageCallBack callBack) {
        RequestQueue queue = Volley.newRequestQueue(context);
        ImageRequest imageRequest = new ImageRequest(url,
                new Response.Listener<Bitmap>() {
                    @Override
                    public void onResponse(Bitmap response) {
                        callBack.onSuccess(response);
                    }
                }, 0, 0, null,
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {

                    }
                });
        queue.add(imageRequest);
    }


}
