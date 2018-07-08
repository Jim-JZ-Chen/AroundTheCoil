using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinkyInput : MonoBehaviour {

	public SlinkyReferences master;

	public int playerID;

	void Update ()
    {
       //input is always 1 or -1, resting at -1 || this is the input for Ps4 left trigger -> 4th axis
       // print("Raw input = "+Input.GetAxisRaw("Tex_Debug_TriggerLower"));
       // print("Smooth input = " + Input.GetAxis("Tex_Debug_TriggerLower"));

        switch (playerID)
        {
            case 1:
                MovementP1();
                break;
            case 2:
                MovementP2();
                break;
			  case 3:
                MovementP3();
                break;
			case 4:
                MovementP4();
                break;
        }
	}


    public void MovementP1()
    {

		if (Input.GetAxis("L_XAxis_1") != 0)
		{
			master.controller.Move(new Vector2(Input.GetAxis("L_XAxis_1"), 0));
		}

		if (Input.GetAxis("L_YAxis_1") != 0)
		{
			master.controller.Move(new Vector2(0, -Input.GetAxis("L_YAxis_1")));
		}

		if (Input.GetAxis("DPad_XAxis_1") != 0)
		{
			master.controller.Move(new Vector2(Input.GetAxis("DPad_XAxis_1"), 0));
		}

		if (Input.GetAxis("DPad_YAxis_1") != 0)
		{
			master.controller.Move(new Vector2(0, Input.GetAxis("DPad_YAxis_1")));
		}

		if (Input.GetButton("LB_1"))
		{
			//master.flip.setControlNodeGrabbing (true);
			master.controller.Fly();
		}
		else
		{
			//master.flip.setControlNodeGrabbing (false);
			master.controller.NoFly();
		}

        if (Input.GetAxisRaw("Tex_Debug_TriggerLower") >= 0)
        {
            //grabbing
            master.flip.setControlNodeGrabbing(true);

        }else {
            //not grabbing
            master.flip.setControlNodeGrabbing(false);
        }


    }



    public void MovementP2()
	{
		if (Input.GetAxis("R_XAxis_1") != 0)
		{
			master.controller.Move(new Vector2(Input.GetAxis("R_XAxis_1"), 0));
		}

		if (Input.GetAxis("R_YAxis_1") != 0)
		{
			master.controller.Move(new Vector2(0, -Input.GetAxis("R_YAxis_1")));
		}



		if (Input.GetButton("A_1"))
		{
			master.controller.Move(new Vector2(0, -1));
		}

		if (Input.GetButton("Y_1"))
		{
			master.controller.Move(new Vector2(0, 1));
		}

		if (Input.GetButton("X_1"))
		{
			master.controller.Move(new Vector2(1, 0));
		}

		if (Input.GetButton("B_1"))
		{
			master.controller.Move(new Vector2(-1, 0));
		}


		if (Input.GetButton("RB_1"))
		{
			master.controller.Fly();
		}
		else
		{
			master.controller.NoFly();
		}
    }


    public void MovementP3()
    {

		if (Input.GetAxis("L_XAxis_2") != 0)
		{
			master.controller.Move(new Vector2(Input.GetAxis("L_XAxis_2"), 0));
		}

		if (Input.GetAxis("L_YAxis_2") != 0)
		{
			master.controller.Move(new Vector2(0, -Input.GetAxis("L_YAxis_2")));
		}

		if (Input.GetAxis("DPad_XAxis_2") != 0)
		{
			master.controller.Move(new Vector2(Input.GetAxis("DPad_XAxis_2"), 0));
		}

		if (Input.GetAxis("DPad_YAxis_2") != 0)
		{
			master.controller.Move(new Vector2(0, Input.GetAxis("DPad_YAxis_2")));
		}

		if (Input.GetButton("LB_2"))
		{
			master.controller.Fly();
		}
		else
		{
			master.controller.NoFly();
		}
    }


    public void MovementP4()
    {
		if (Input.GetAxis("R_XAxis_2") != 0)
		{
			master.controller.Move(new Vector2(Input.GetAxis("R_XAxis_2"), 0));
		}

		if (Input.GetAxis("R_YAxis_2") != 0)
		{
			master.controller.Move(new Vector2(0, -Input.GetAxis("R_YAxis_2")));
		}



		if (Input.GetButton("A_2"))
		{
			master.controller.Move(new Vector2(0, -1));
		}

		if (Input.GetButton("Y_2"))
		{
			master.controller.Move(new Vector2(0, 1));
		}

		if (Input.GetButton("X_2"))
		{
			master.controller.Move(new Vector2(1, 0));
		}

		if (Input.GetButton("B_2"))
		{
			master.controller.Move(new Vector2(-1, 0));
		}


		if (Input.GetButton("RB_2"))
		{
			master.controller.Fly();
		}
		else
		{
			master.controller.NoFly();
		}
    }
}
