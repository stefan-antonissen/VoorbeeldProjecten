package com.example.stefa.hueapp;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by stefa on 11/20/2016.
 */

public class LightContent {

    public static final List<Light> ITEMS = new ArrayList<Light>();

    public static final Map<String, Light> ITEM_MAP = new HashMap<String, Light>();

    private static void addItem(Light item) {
        ITEMS.add(item);
        ITEM_MAP.put("" + item.id, item);
    }
}
