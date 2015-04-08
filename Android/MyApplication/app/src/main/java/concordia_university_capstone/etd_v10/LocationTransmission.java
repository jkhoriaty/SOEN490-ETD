package concordia_university_capstone.etd_v10;

import android.content.Intent;
import android.location.Location;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;


public class LocationTransmission extends ActionBarActivity
{
	String deviceID;
	String serverIP;
	int serverPort;

    Button locationTransmission;
    Button backToLogin;
    CheckBox checkBox;
    Boolean isBroadCasting = false;
    Intent serviceIntent;

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_location_transmission);

		Intent intent = getIntent();
		deviceID = intent.getStringExtra("deviceID");
		serverIP = intent.getStringExtra("serverIP");
		serverPort = intent.getIntExtra("serverPort", -1);

        locationTransmission = (Button) findViewById(R.id.button1);
        //checkBox = (CheckBox) findViewById(R.id.checkBox1);
        //backToLogin = (Button) findViewById(R.id.backButton);

        locationTransmission.setOnClickListener(new View.OnClickListener() {
            public void onClick (View v){
                if(!isBroadCasting){
                    isBroadCasting = true;
                    locationTransmission.setText("Stop Transmitting Location");
                    startTransmitting();
                }
                else{
                    isBroadCasting = false;
                    locationTransmission.setText("Start Transmitting Location");
                    stopTransmitting();
                }
            }
        });
	}

    public void Back (View view)
    {
        checkBox.setChecked(false);
        setContentView(R.layout.activity_login);
    }

    private void startTransmitting() {
        try{
            startService(serviceIntent);
        }
        catch (Exception e)
        {
            e.printStackTrace();
            Toast.makeText(getApplicationContext(), e.getClass().getName() + " " + e.getMessage(), Toast.LENGTH_LONG).show();
        }
    }

    private void stopTransmitting(){
        try
        {
            stopService(serviceIntent);
            checkBox.setChecked(false);
        }
        catch (Exception e)
        {
            e.printStackTrace();
            checkBox.setChecked(true);
            Toast.makeText(getApplicationContext(), e.getClass().getName() + " " + e.getMessage(), Toast.LENGTH_LONG).show();
        }
    }

	@Override
	protected void onStart()
	{
		super.onStart();
        try {
            serviceIntent = new Intent(this, BackgroundLocation.class);
	        serviceIntent.putExtra("deviceID", deviceID);
	        serviceIntent.putExtra("serverIP", serverIP);
	        serviceIntent.putExtra("serverPort", serverPort);
            startService(serviceIntent);
        }catch (Exception e)
        {
            Toast.makeText(this, "Service can not be created. Please try again!", Toast.LENGTH_SHORT).show();
        }
	}
}
