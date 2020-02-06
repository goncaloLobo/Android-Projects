package guigue.chris.whack_a_mole;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;

public class MainMenu extends AppCompatActivity {
    private Bundle extras;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main_menu);

        //Set on click listener for the play button
        Button play = (Button)findViewById(R.id.btnPlay);
        play.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplication(), Game.class);
                if(extras != null)
                {
                    intent.putExtras(extras);
                }
                startActivity(intent);
            }
        });

        //set on click listener for the options button
        Button options = (Button)findViewById(R.id.btnOptions);
        options.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplication(), Options.class);
                if(extras != null)
                {
                    intent.putExtras(extras);
                }
                startActivityForResult(intent, 200);
            }
        });

        //set on click listener for the about button
        Button about = (Button)findViewById(R.id.btnAbout);
        about.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplication(), About.class);
                startActivity(intent);
            }
        });

        //set on click listener for the highscores button
        Button highscores = (Button)findViewById(R.id.btnHighScores);
        highscores.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplication(), HighScores.class);
                startActivity(intent);
            }
        });


    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data)
    {
        super.onActivityResult(requestCode, resultCode, data);
        if(data.getExtras() != null)
        {
            extras = data.getExtras();
        }
    }
}
