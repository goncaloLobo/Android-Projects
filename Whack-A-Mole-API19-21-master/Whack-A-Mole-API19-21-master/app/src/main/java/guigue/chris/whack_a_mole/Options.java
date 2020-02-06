package guigue.chris.whack_a_mole;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Spinner;
import java.util.List;

public class Options extends AppCompatActivity {

    private String userName = "Anonymous";
    private String startingLives;
    private String bonusLives;
    private int[] buttons = {R.drawable.roundbuttonblack, R.drawable.roundbuttonwhite, R.drawable.roundbuttonblue, R.drawable.roundbuttonpurple, R.drawable.roundbuttongreen, R.drawable.roundbuttonorange, R.drawable.roundbuttonred, R.drawable.roundbuttonyellow};
    private int[] colours = {R.color.black, R.color.white, R.color.darkblue, R.color.darkpurple, R.color.darkgreen, R.color.darkorange, R.color.darkred, R.color.yellow};
    public SharedPreferences sharedPreferences;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_options);
        LoadSpinners();
        LoadListeners();

        sharedPreferences = getSharedPreferences("WhackAMole",Context.MODE_PRIVATE);
        for(int i = 0; i < colours.length; ++i)
        {
            int temp =  sharedPreferences.getInt("BackGroundColour", R.color.darkgreen);
            if(colours[i] == temp)
            {
                Spinner spnBackColour = (Spinner)findViewById(R.id.spnBackGroundColour);
                spnBackColour.setSelection(i);
            }
            temp =  sharedPreferences.getInt("MoleHoleColour", R.color.black);
            if(buttons[i] == temp)
            {
                Spinner spnHoleColour = (Spinner)findViewById(R.id.spnHoleColour);
                spnHoleColour.setSelection(i);
            }
        }
    }

    public void LoadSpinners()
    {
        //Load Users Spinner
        DatabaseHandler db = new DatabaseHandler(getApplicationContext());
        Spinner spnUsers = (Spinner)findViewById(R.id.spnUsers);
        List<String> users = db.getUsers();
        users.add(0, "Select User");
        users.add("Add New User");
        ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_dropdown_item, users);
        spnUsers.setAdapter(adapter);

        //Load background Colours
        Spinner spnBackColour = (Spinner)findViewById(R.id.spnBackGroundColour);
        ArrayAdapter adapterColours = ArrayAdapter.createFromResource(this, R.array.colors, android.R.layout.simple_list_item_1);
        spnBackColour.setAdapter(adapterColours);

        //Load Hole Colours
        Spinner spnHoleColour = (Spinner)findViewById(R.id.spnHoleColour);
        ArrayAdapter adapterHoles = ArrayAdapter.createFromResource(this, R.array.colors, android.R.layout.simple_list_item_1);
        spnHoleColour.setAdapter(adapterHoles);

        Spinner spnLives = (Spinner)findViewById(R.id.spnStartingLives);
        ArrayAdapter adapterLives = ArrayAdapter.createFromResource(this, R.array.startingLives, android.R.layout.simple_list_item_1);
        spnLives.setAdapter(adapterLives);

        Spinner spnBonus = (Spinner)findViewById(R.id.spnBonusLives);
        ArrayAdapter adapterBonus = ArrayAdapter.createFromResource(this, R.array.bonusLives, android.R.layout.simple_list_item_1);
        spnBonus.setAdapter(adapterBonus);
    }

    public void LoadListeners()
    {
        //Users Spinner Listener
        Spinner spnUsers = (Spinner)findViewById(R.id.spnUsers);
        spnUsers.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                String value = adapterView.getItemAtPosition(i).toString();
                LinearLayout llUser = (LinearLayout) findViewById(R.id.llAddUser);
                switch (value) {
                    case "Add New User":
                        llUser.setVisibility(View.VISIBLE);
                        break;

                    case "Select User":
                        llUser.setVisibility(View.GONE);
                        break;
                    default:
                        userName = value;
                        llUser.setVisibility(View.GONE);
                        break;
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
            }
        });//spnUsers.onItemSelected()

        //Load background colour listener
        Spinner spnBackColour = (Spinner)findViewById(R.id.spnBackGroundColour);
        spnBackColour.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                sharedPreferences = getSharedPreferences("WhackAMole",Context.MODE_PRIVATE);
                sharedPreferences.edit().putInt("BackGroundColour", colours[i] ).apply();
            }
            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
            }
        });//spnBackGroundColour()

        //Load mole holes colours listener
        Spinner spnHoleColour = (Spinner)findViewById(R.id.spnHoleColour);
        spnHoleColour.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                sharedPreferences = getSharedPreferences("WhackAMole",Context.MODE_PRIVATE);
                sharedPreferences.edit().putInt("MoleHoleColour", buttons[i]).apply();
            }
            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
            }
        });//Load hole colours listener

        //Load starting lives listener
        Spinner spnLives = (Spinner)findViewById(R.id.spnStartingLives);
        spnLives.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                startingLives = adapterView.getItemAtPosition(i).toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });//Load lives listener

        //Load bonus lives spinner listener
        Spinner spnBonus = (Spinner)findViewById(R.id.spnBonusLives);
        spnBonus.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                bonusLives = adapterView.getItemAtPosition(i).toString();
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });//Bonus LIves Listener()


        Button addUser = (Button)findViewById(R.id.btnCommit);
        addUser.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onCommitUser();
            }
        });//Add User Click()

        //Save button Listener
        Button save = (Button)findViewById(R.id.btnSave);
        save.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onSave();
            }
        });
    }//LoadListeners()

    public void onSave()
    {
        Bundle extras = new Bundle();
        Intent intent = new Intent();

        extras.putString("StartingLives", startingLives);
        extras.putString("BonusLives", bonusLives);

        sharedPreferences.edit().putString("user", userName).apply();
        intent.putExtras(extras);
        setResult(RESULT_OK, intent);
        finish();
    }

    public void onCommitUser()
    {
        DatabaseHandler db = new DatabaseHandler(getApplicationContext());
        EditText user = (EditText)findViewById(R.id.etUser);
        if(user.getText().length() < 3)
        {
            return;
        }
        userName = user.getText().toString();
        db.InsertUser(user.getText().toString());
        LoadSpinners();
        user.setVisibility(View.GONE);
        Button addUser = (Button)findViewById(R.id.btnCommit);
        addUser.setVisibility(View.GONE);
    }

    @Override
    public void onBackPressed() {
        onSave();
        super.onBackPressed();

    }

}
