package guigue.chris.whack_a_mole;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.os.Bundle;
import android.app.Fragment;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.GridView;
import android.widget.ImageView;

/**
 * A simple {@link Fragment} subclass.
 */
public class MolesFragment extends Fragment {

    int gridSize = 4;
    private SharedPreferences prefs;
    private MoleController controller;
    //   private MoleController controller;

    public MolesFragment() {
        // Required empty public constructor
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View vw = inflater.inflate(R.layout.fragment_moles, container, false);

        GridView moleHoles = (GridView)vw.findViewById(R.id.gvMoleHoles);
        moleHoles.setGravity(Gravity.CENTER);
        if(getActivity().getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT)
        {
            moleHoles.setNumColumns(4);
        }else
        if(getActivity().getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE)
        {
            moleHoles.setNumColumns(8);
        }
        moleHoles.setAdapter(new HoleAdapter(getActivity()));

        return vw;
    }

    public class HoleAdapter extends BaseAdapter
    {
        private Context mContext;

        public HoleAdapter(Context c) {
            mContext = c;
        }

        public int getCount() {
            return gridSize * gridSize;
        }

        public Object getItem(int position) {
            return null;
        }

        public long getItemId(int position) {
            return 0;
        }

        // create a new HoleView for each item referenced by the Adapter
        public View getView(int position, View vw, ViewGroup parent)
        {
            ImageView btn;
            if (vw == null) {
                // if it's not recycled, initialize some attributes
                btn = new ImageView(mContext);

            } else {
                btn = (ImageView) vw;

            }
            prefs = getActivity().getSharedPreferences("WhackAMole", Context.MODE_PRIVATE);
            if(getResources().getConfiguration().orientation == Configuration.ORIENTATION_LANDSCAPE)
            {
                btn.setLayoutParams(new GridView.LayoutParams(220, 220));
            }
            else
            if(getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT)
            {
                btn.setLayoutParams(new GridView.LayoutParams(240, 240));
            }
            btn.setPadding(3, 3, 3, 3);

            btn.setId(position);
            btn.setCropToPadding(true);
            btn.setBackgroundResource(prefs.getInt("MoleHoleColour", R.drawable.roundbuttonblack));
            btn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    controller.CheckHoleForMole((ImageView)v);

                }
            });
            return btn;
        }
    }

    public interface MoleController
    {
        void CheckHoleForMole(ImageView mole);
    }

    public void onAttach(Activity activity)
    {
        super.onAttach(activity);
        try
        {
            controller = (MoleController)activity;
        }
        catch(ClassCastException e)
        {
            throw new ClassCastException(activity.toString());
        }
    }//onAttach(Activity)
}