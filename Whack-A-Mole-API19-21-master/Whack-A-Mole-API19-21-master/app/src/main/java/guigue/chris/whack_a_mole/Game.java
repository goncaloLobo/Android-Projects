package guigue.chris.whack_a_mole;

import android.app.Activity;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.app.ActionBar;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.support.v4.widget.DrawerLayout;
import android.widget.GridView;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Random;
import java.util.Timer;
import java.util.TimerTask;

public class Game extends AppCompatActivity
        implements NavigationDrawerFragment.NavigationDrawerCallbacks, MolesFragment.MoleController  {
    private SharedPreferences prefs;
    private int startingLives;
    private Bundle extras;
    private int bonusLives;
    private GridView moleHoles;
    private List<Boolean> activeMoles;
    private int numMoles;
    private int molesHit;
    private int gameSpeed;
    private Boolean hasFinished;
    private Timer timer;
    private String user;
    /**
     * Fragment managing the behaviors, interactions and presentation of the navigation drawer.
     */
    private NavigationDrawerFragment mNavigationDrawerFragment;

    /**
     * Used to store the last screen title. For use in {@link #restoreActionBar()}.
     */
    private CharSequence mTitle;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_game);

        mNavigationDrawerFragment = (NavigationDrawerFragment)
                getSupportFragmentManager().findFragmentById(R.id.navigation_drawer);
        mTitle = getTitle();
        extras = getIntent().getExtras();
        prefs = getSharedPreferences("WhackAMole", Context.MODE_PRIVATE);

        //set a task to start teh game once everything has loaded up
        TimerTask task = new TimerTask() {
            @Override
            public void run() {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        SetUpGame();
                    }
                });
            }
        };
        timer = new Timer();
        timer.schedule(task, 1000);

        // Set up the drawer.
        mNavigationDrawerFragment.setUp(
                R.id.navigation_drawer,
                (DrawerLayout) findViewById(R.id.drawer_layout));
    }

    //Decrement a life
    public void DecrementLife()
    {
        startingLives = startingLives - 1;
        TextView tvLives = (TextView)findViewById(R.id.tvLives);
        tvLives.setText(startingLives + "");
    }

    //Increment a life
    public void IncrementLife()
    {
        startingLives = startingLives + 1;
        TextView tvLives = (TextView)findViewById(R.id.tvLives);
        tvLives.setText(startingLives + "");
    }

    //Handle the different screen orientations
    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);
        moleHoles = (GridView)findViewById(R.id.gvMoleHoles);
        if(newConfig.orientation == Configuration.ORIENTATION_PORTRAIT)
        {
            moleHoles.setNumColumns(4);
            View molesHoles = findViewById(R.id.fragMoleHoles);
            molesHoles.setLayoutParams(new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.MATCH_PARENT, 2));
            View scoreBoard = findViewById(R.id.fragScoreBoard);
            scoreBoard.setLayoutParams(new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.MATCH_PARENT, 5));

            View heart = findViewById(R.id.viewHeart);
            RelativeLayout.LayoutParams params = (RelativeLayout.LayoutParams)heart.getLayoutParams();
            params.setMargins(0, 0, 50, 80);

        }else
        if(newConfig.orientation == Configuration.ORIENTATION_LANDSCAPE)
        {
            moleHoles.setNumColumns(8);
            View molesHoles = findViewById(R.id.fragMoleHoles);
            molesHoles.setLayoutParams(new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.MATCH_PARENT, 2));
            View scoreBoard = findViewById(R.id.fragScoreBoard);
            scoreBoard.setLayoutParams(new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.MATCH_PARENT, 3));

            View heart = findViewById(R.id.viewHeart);
            RelativeLayout.LayoutParams params = (RelativeLayout.LayoutParams)heart.getLayoutParams();
            params.setMargins(0, 0, 170, 30);
        }
    }

    @Override
    public void onNavigationDrawerItemSelected(int position)
    {
        // update the main content by replacing fragments
        FragmentManager fragmentManager = getSupportFragmentManager();
        fragmentManager.beginTransaction()
                .replace(R.id.container, PlaceholderFragment.newInstance(position + 1))
                .commit();
    }

    //Interface method, called on click of ImageView
    public void CheckHoleForMole(ImageView mole)
    {
        int position = mole.getId();
        if(activeMoles.get(position))
        {
            MoleHit();
            mole.setBackgroundResource(prefs.getInt("MoleHoleColour", R.drawable.roundbuttonblack));
            activeMoles.set(position, false);

        }
        else
        {
            DecrementLife();
        }
    }

    //Setup the basics for the Whack a mole game
    public void SetUpGame()
    {
        activeMoles = new ArrayList<>();
        moleHoles = (GridView)findViewById(R.id.gvMoleHoles);
        user = prefs.getString("user", "Anonymous");
        molesHit = 0;
        numMoles = 1;
        gameSpeed = 1000;
        hasFinished = false;

        if(extras != null)
        {
            startingLives = Integer.parseInt(extras.getString("StartingLives", "10"));
            bonusLives = Integer.parseInt(extras.getString("BonusLives", "10"));
        }
        else
        {
            startingLives = 10;
            bonusLives = 20;
        }

        TextView tvLives = (TextView)findViewById(R.id.tvLives);
        tvLives.setText(startingLives + "");

        for(int i = 0; i < 16; ++i)
        {
            activeMoles.add(i,false);
        }
        StartGame();
    }

    //StartGame will call Play at a fixed rate based on GameSpeed
    public void StartGame()
    {
        TimerTask task = new TimerTask() {
            @Override
            public void run() {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Play();
                    }
                });
            }
        };
        timer.scheduleAtFixedRate(task, new Date(), gameSpeed * 3);

    }

    public void Play()
    {
        CheckMolesHit();

        for(int i = 0; i< numMoles; ++ i)
        {
            if(startingLives > 0)
            {
                TimerTask run = new TimerTask() {
                    @Override
                    public void run() {
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                PeakMole();
                            }
                        });
                    }
                };
                timer.schedule(run, gameSpeed);
            }
            else
            {
                if(!hasFinished)
                {
                    FinishGame();
                }
                else
                {
                    return;
                }
            }
        }
    }

    //What happens when a mole is hit
    public void MoleHit()
    {
        ++molesHit;
        TextView tvHits = (TextView)findViewById(R.id.tvMolesHit);
        tvHits.setText(molesHit + "");
    }

    public void FinishGame()
    {
        hasFinished = true;
        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(
                this);

        // set title
        alertDialogBuilder.setTitle("Game Over");

        // set dialog message
        alertDialogBuilder
                .setMessage("Congratulations you killed " + molesHit + " moles. \r\n Would you like to play again?")
                .setCancelable(false)
                .setPositiveButton("Yes",new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog,int id) {
                        // if this button is clicked, close
                        // current activity
                        recreate();
                    }
                })
                .setNegativeButton("No", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        // if this button is clicked, just close
                        // the dialog box and do nothing
                        finish();
                    }
                });

        // create alert dialog
        AlertDialog alertDialog = alertDialogBuilder.create();

        // show it
        alertDialog.show();

        DatabaseHandler db = new DatabaseHandler(this);
        db.InsertScore(user, molesHit + "");
    }

    //If user changes settings mid game
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data)
    {
        super.onActivityResult(requestCode, resultCode, data);
        if(data.getExtras() != null)
        {
            extras = data.getExtras();
        }
    }

    //Check the number of moles hit to see if I need to grant a life, increment game speed, reduce number of moles in play or add a mole to play
    public void CheckMolesHit()
    {
        if(molesHit > 0)
        {
            if(molesHit % 7 == 0)
            {
                IncrementGameSpeed();
                Toast toast = Toast.makeText(this, "Speed Increased!", 1000);
                toast.show();
            }

            if(molesHit % bonusLives == 0)
            {
                IncrementLife();
                Toast toast = Toast.makeText(this, "Bonus life granted!", 1000);
                toast.show();
            }

            Random rand = new Random();
            int temp = (int)(rand.nextDouble() * 15);
            if(temp < 1)
            {
                temp = 1;
            }

            if(molesHit % temp == 0)
            {
                numMoles++;
            }

            temp = (int)(rand.nextDouble() * 7);
            if(temp < 2)
            {
                temp = 2;
            }
            if(numMoles > 8)
            {
                numMoles /= temp;
                Toast toast = Toast.makeText(this, "Moles Reset!", 1000);
                toast.show();
            }

        }
    }

    //Increment the game speed text UI, and decrement the game speed delay call between moles
    public void IncrementGameSpeed()
    {
        TextView tvGameSpeed = (TextView) findViewById(R.id.tvGameSpeed);
        gameSpeed = (int)(gameSpeed * .8);
        Double temp = Double.parseDouble(tvGameSpeed.getText().toString().substring(1));
        temp += .25;
        tvGameSpeed.setText("x" + temp);

    }

    //Choose what hole to peak a mole out of, will call itself if a mole already exists showing at that location
    public void PeakMole()
    {
        Random rand = new Random();
        int location = (int)(rand.nextDouble() * 16);
        if(!activeMoles.get(location))
        {
            showMole(location);
        }
        else
        {
            PeakMole();
        }
    }

    //Show the mole that was chosen from PeakMole() and set random delay call to hideMole()
    public void showMole(final int position)
    {
        ImageView currentMole = (ImageView)moleHoles.getChildAt(position);
        Random rand = new Random();
        int delay = (int)(rand.nextDouble() * 3000);
        currentMole.setBackgroundResource(R.drawable.mole);
        activeMoles.set(position, true);
        Timer timer = new Timer();
        TimerTask task = new TimerTask(){
            @Override
            public void run() {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        hideMole(position);
                    }
                });
            }
        };

        timer.schedule(task, delay);
    }

    //Hide the mole at position and set to false in the array
    public void hideMole(int position)
    {
        ImageView currentMole = (ImageView)moleHoles.getChildAt(position);
        prefs = getSharedPreferences("WhackAMole", Context.MODE_PRIVATE);
        activeMoles.set(position, false);
        currentMole.setBackgroundResource(prefs.getInt("MoleHoleColour", R.drawable.roundbuttonblack));
    }

//    //onResuming from the options check for a changed user
//    @Override
//    protected void onResume()
//    {
//        prefs = getSharedPreferences("WhackAMole", Context.MODE_PRIVATE);
//        if(!user.equals(prefs.getString("user", "Anonymous")))
//        {
//            recreate();
//        }
//        super.onResume();
//    }

    //NavDrawer stuff TODO setup NavDrawer, not sure I will have time*
    public void onSectionAttached(int number) {
        switch (number) {
            case 1:
                mTitle = getString(R.string.title_section1);
                break;
            case 2:
                mTitle = getString(R.string.title_section2);
                break;
            case 3:
                mTitle = getString(R.string.title_section3);
                break;
        }
    }

    public void restoreActionBar() {
        ActionBar actionBar = getSupportActionBar();
        actionBar.setNavigationMode(ActionBar.NAVIGATION_MODE_STANDARD);
        actionBar.setDisplayShowTitleEnabled(true);
        actionBar.setTitle(mTitle);
    }


    /**
     * A placeholder fragment containing a simple view.
     */
    public static class PlaceholderFragment extends Fragment {
        /**
         * The fragment argument representing the section number for this
         * fragment.
         */
        private static final String ARG_SECTION_NUMBER = "section_number";
        private SharedPreferences prefs;
        /**
         * Returns a new instance of this fragment for the given section
         * number.
         */
        public static PlaceholderFragment newInstance(int sectionNumber) {
            PlaceholderFragment fragment = new PlaceholderFragment();
            Bundle args = new Bundle();
            args.putInt(ARG_SECTION_NUMBER, sectionNumber);
            fragment.setArguments(args);
            return fragment;
        }

        public PlaceholderFragment() {
        }

        @Override
        public View onCreateView(LayoutInflater inflater, ViewGroup container,
                                 Bundle savedInstanceState) {
            View vw = inflater.inflate(R.layout.game_activity_fragment, container, false);

            Initialize(vw);

            return vw;
        }

        public void Initialize(View vw)
        {
            prefs = getActivity().getSharedPreferences("WhackAMole",Context.MODE_PRIVATE);
            vw.setBackgroundResource(prefs.getInt("BackGroundColour", R.color.darkgreen));
        }

        @Override
        public void onAttach(Activity activity) {
            super.onAttach(activity);
            ((Game) activity).onSectionAttached(
                    getArguments().getInt(ARG_SECTION_NUMBER));
        }
    }

}
