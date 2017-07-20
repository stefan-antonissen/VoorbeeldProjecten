package com.example.stefanantonissen.contactcard;


import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;
/**
 * Created by Stefan Antonissen on 11/10/2016.
 */

public class PersonAdapter extends BaseAdapter {

    Context mContext;
    LayoutInflater mInflator;
    ArrayList mPersonArrayList;
    private ViewHolder viewHolder;
    private HttpHandler handler = new HttpHandler();

    public PersonAdapter(Context context, LayoutInflater layoutInflater, ArrayList<Person> personArrayList)
    {
        mContext = context;
        mInflator = layoutInflater;
        mPersonArrayList = personArrayList;
    }

    @Override
    public int getCount() {
        int size = mPersonArrayList.size();
        Log.i("getCount()","=" + size);
        return size;
    }

    @Override
    public Object getItem(int position) {
        Log.i("getItem()","");
        return mPersonArrayList.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        // Create new of gebruik een al bestaande (recycled by Android)
        if(convertView == null) {

            //
            convertView = mInflator.inflate(R.layout.listview_row, null);

            //
            viewHolder = new ViewHolder();
            viewHolder.imageView = (ImageView) convertView.findViewById(R.id.imageView);
            viewHolder.name = (TextView) convertView.findViewById(R.id.name);
            viewHolder.email = (TextView) convertView.findViewById(R.id.email);

            //
            convertView.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }

        // En nu de viewHolder invullen met de juiste persons
        Person person = (Person) mPersonArrayList.get(position);

        viewHolder.name.setText(person.name);
        viewHolder.email.setText(person.email);

        if (viewHolder.imageView != null)
        {
            new ImageDownloader(viewHolder.imageView).execute(person.bitmap);
        }


//        handler.getImage(mContext, person.bitmap, new ImageCallBack() {
//            @Override
//            public void onSuccess(Bitmap bitmap) {
//                viewHolder.imageView.setImageBitmap(bitmap);
//
//            }
//        });

        return convertView;
    }

    // Holds all data to the view. Wordt evt. gerecycled door Android
    private static class ViewHolder {
        public ImageView imageView;
        public TextView name;
        public TextView email;
    }
}
