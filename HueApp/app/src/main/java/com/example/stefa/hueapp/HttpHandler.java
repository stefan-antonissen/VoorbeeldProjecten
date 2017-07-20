package com.example.stefa.hueapp;

import android.app.DownloadManager;
import android.content.Context;
import android.util.Log;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.stefa.hueapp.Light;
import com.example.stefa.hueapp.ServerCallBack;

import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by stefa on 11/20/2016.
 */

public class HttpHandler {

    //private String address = "http://10.0.1.34";
    private String address = "http://192.168.1.179";
    private String username = "8lH2OpcY5xDfKJbKquBr8WG3CfVk5Xs9ParQSqLX";
    //private String username = "F7FDcz-ygySO4GXLwNJl6O1Lhu6kQZ9tuEFnZpwm";


    public void getAllLights(Context context, String url, final ServerCallBack callback)
    {
        final ArrayList<Light> lights = new ArrayList<Light>();
        String url2 = address + "/api/" + username + "/lights";
        RequestQueue queue = Volley.newRequestQueue(context);
        //String url = "https://randomuser.me/api";
        StringRequest objectRequest = new StringRequest(Request.Method.GET, url2,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        try {
                            JSONObject obj = new JSONObject(response.toString());
                            JSONArray tempLights = obj.names();
                            for (int i = 0; i < tempLights.length(); i++) {
                                JSONObject lightObject = obj.getJSONObject(tempLights.getString(i));
                                Light light = new Light();
                                if (lightObject.getJSONObject("state").getBoolean("reachable") == true)
                                {
                                    light.power = lightObject.getJSONObject("state").getBoolean("on");
                                    light.name = lightObject.get("name").toString();
                                    light.type = lightObject.get("type").toString();
                                    light.bri = lightObject.getJSONObject("state").getInt("bri");
                                    light.hue = lightObject.getJSONObject("state").getInt("hue");
                                    light.sat = lightObject.getJSONObject("state").getInt("sat");
                                    light.id = Integer.parseInt(tempLights.get(i).toString());
                                    lights.add(light);
                                }
                            }
                            callback.onSuccess(lights);
                        } catch (Exception e) {
                            e.printStackTrace();
                        }

                    }

                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {

            }
        });
        queue.add(objectRequest);
    }

    public void setLightState(Context context, final Light light) {
        String url = address + "/api/" + username + "/lights/" + light.id + "/state";
        RequestQueue queue = Volley.newRequestQueue(context);

        try {
            JSONObject obj = new JSONObject();
            obj.put("hue", light.hue);
            obj.put("bri", light.bri);
            obj.put("sat", light.sat);
            obj.put("on", light.power);


            JsonObjectRequest request1 = new JsonObjectRequest(Request.Method.PUT, url, obj, new Response.Listener<JSONObject>() {
                @Override
                public void onResponse(JSONObject response) {

                }}, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        error.printStackTrace();
                    }
                });
            queue.add(request1);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}

