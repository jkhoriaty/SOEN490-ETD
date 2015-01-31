package concordia_university_capstone.etd_v10;

import android.content.Intent;
import android.location.Location;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;


public class LocationTransmission extends ActionBarActivity implements GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener, LocationListener
{
	String deviceID;
	String serverIP;
	int serverPort;

	GoogleApiClient apiClient;
	LocationRequest locationRequest;

	@Override
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_location_transmission);

		Intent intent = getIntent();
		deviceID = intent.getStringExtra("deviceID");
		serverIP = intent.getStringExtra("serverIP");
		serverPort = intent.getIntExtra("serverPort", -1);

		buildGoogleApiClient();

		createLocationRequest();
	}

	protected synchronized void buildGoogleApiClient()
	{
		apiClient = new GoogleApiClient.Builder(this)
				.addConnectionCallbacks(this)
				.addOnConnectionFailedListener(this)
				.addApi(LocationServices.API)
				.build();
	}

	protected void createLocationRequest()
	{
		locationRequest = new LocationRequest();
		locationRequest.setInterval(10000);
		locationRequest.setFastestInterval(1000);
		locationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
	}

	@Override
	protected void onStart()
	{
		super.onStart();
		apiClient.connect();
	}

	@Override
	protected void onStop()
	{
		super.onStop();
		if(apiClient.isConnected())
		{
			apiClient.disconnect();
		}
	}

	@Override
	public void onConnected(Bundle connectionHint)
	{
		Log.i("ConnectionTestProcess", "Connection succeeded");
	}

	@Override
	public void onConnectionFailed(ConnectionResult result)
	{
		Log.i("ConnectionTestProcess", "Connection failed: ConnectionResult.getErrorCode() = " + result.getErrorCode());
	}

	@Override
	public void onConnectionSuspended(int cause)
	{
		Log.i("ConnectionTestProcess", "Connection suspended");
		apiClient.connect();
	}

	public void startLocationUpdates(View view)
	{
		LocationServices.FusedLocationApi.requestLocationUpdates(apiClient, locationRequest, this);
	}

	@Override
	public void onLocationChanged(Location location)
	{
		new Thread(new LocationSenderThread(serverIP, serverPort, deviceID, location)).start();
	}
}
