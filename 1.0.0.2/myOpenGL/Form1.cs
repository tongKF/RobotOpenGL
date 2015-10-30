using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenGL;
using System.Runtime.InteropServices;
using System.Reflection; 

namespace myOpenGL
{

    public partial class Form1 : Form
    {
       
        #region Mouse Controls
        Point CurrentMousePosition;
        
        //indicates mouse left button pressed down
        bool LMBD = false;
        //indicates mouse right button pressed down
        bool RMBD = false;


        // last and current number of deltas (mouse wheel rotation)
        float lastDelta = 0;
        float currDelta = 0;
        
        //step value in the z axis 
        const int WheelStep = 15;
        //step value in the x,y axis
        const int RMBStep = 3;
        #endregion

        cOGL cGL;
        bool flagForAnotherStep = false;
        int animationCount = 0, animationCountOfSwordAttack = 0;
        float animationTime = 33, walkSpeed = 4.0f;
        // flag to enter timer one time per crash
        bool crashFlagForTimer = false;

        public bool IsPowerJump = false;
        
        public Form1()
        {
            //enable the keys events
            this.KeyPreview = true;

            InitializeComponent();

            //update version number in the Form Label
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            this.Text = "My Robot " + version.ToString();

            this.MouseWheel += new MouseEventHandler(panel1_MouseWheel);
            
            
            cGL = new cOGL(panel1);

            //initialize timers
            timer1.Enabled = true;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;


            timer3.Interval = 1;
            timer4.Interval = 1;


            //apply the bars values as cGL.ScrollValue[..] properties 
            hScrollBarScroll(hScrollBar2, null);
            hScrollBarScroll(hScrollBar3, null);
            hScrollBarScroll(hScrollBar8, null);
            hScrollBarScroll(hScrollBar11, null);
            hScrollBarScroll(hScrollBar12, null);
            hScrollBarScroll(hScrollBar13, null);

           
        }


        private void panel1_Resize(object sender, EventArgs e)
        {
            panel1.Width = this.Height;
            panel1.Height = this.Height;
            
            cGL.OnResize();
        }

        #region panel1 Mouse Events
        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LMBD = true;
                RMBD = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                LMBD = false;
                RMBD = true;
            }
            else
                return;

            // on mouse down update x,y axis translation, rotation
            cGL.Last_X = cGL.X_Value;
            cGL.Last_Y = cGL.Y_Value;
            cGL.Last_rotY = cGL.RotY;
            cGL.Last_rotX = cGL.RotX;

            // on mouse down mouse position
            CurrentMousePosition = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (LMBD)
            {
                //rotate x and y axis
                float precentOfRotate;
                precentOfRotate = (float)(e.Location.X - CurrentMousePosition.X) / (float)panel1.Width;
                cGL.RotY = cGL.Last_rotY + 360.0f * precentOfRotate;
                precentOfRotate = (float)(e.Location.Y - CurrentMousePosition.Y) / (float)panel1.Height;
                cGL.RotX = cGL.Last_rotX + 360.0f * precentOfRotate;

                cGL.Robot.WalkAngle = cGL.RotY;
            }
            else if (RMBD)
            {
                int mult = 1;
                if (Control.ModifierKeys == Keys.Shift)
                    mult = 3;

                float precentOfTranslate;     

                if (cGL.Z_Value > 3)
                {
                    //if zome in take into account the transformation value in the z axis  
                    precentOfTranslate = (float)(CurrentMousePosition.X - e.Location.X) / (float)panel1.Width;
                    cGL.X_Value = cGL.Last_X + cGL.Z_Value * mult * precentOfTranslate;
                    precentOfTranslate = (float)(CurrentMousePosition.Y - e.Location.Y) / (float)panel1.Height;
                    cGL.Y_Value = cGL.Last_Y - cGL.Z_Value * mult * precentOfTranslate;
                }
                else
                {
                    precentOfTranslate = (float)(CurrentMousePosition.X - e.Location.X) / (float)panel1.Width;
                    cGL.X_Value = cGL.Last_X + RMBStep * mult * precentOfTranslate;
                    precentOfTranslate = (float)(CurrentMousePosition.Y - e.Location.Y) / (float)panel1.Height;
                    cGL.Y_Value = cGL.Last_Y - RMBStep * mult * precentOfTranslate;
                }
            }
        }

        //on mouse wheel - zoom in/out
        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {

            int mult = 1;
            if (Control.ModifierKeys == Keys.Shift)
                mult = 3;

            float precentOfTranslate = 0;
            currDelta += e.Delta;

            if (currDelta != lastDelta)
            {
                if (currDelta > lastDelta)
                    precentOfTranslate = -50.0f / panel1.Height;
                else
                    if (currDelta < lastDelta)
                        precentOfTranslate = 50.0f / panel1.Height;

                if (cGL.Z_Value > 15)
                    cGL.Z_Value = cGL.Z_Value - cGL.Z_Value * mult * precentOfTranslate;
                else
                    cGL.Z_Value = cGL.Z_Value - WheelStep * mult * precentOfTranslate;

                lastDelta = currDelta;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            LMBD = false;
            RMBD = false;
        }
        #endregion


        //this function updates ScrollValue array with the bars values in every change
        private void hScrollBarScroll(object sender, ScrollEventArgs e)
        {
            HScrollBar hb = (HScrollBar)sender;
            int scrollNum = int.Parse(hb.Name.Substring(10));
            cGL.ScrollValue[scrollNum - 1] = (hb.Value - 100) / 10.0f;
            if (e != null)
                cGL.Draw();
        }



        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = this.Width;
            panel1.Height = this.Height;
        }

       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (cGL.Robot.IsJumping) //When jumping,do nothing
                return;

            // check if pressed key for jumping, doing salta, shooting/attacking or powering jump while Robot has weapon
            // '32' - indicates Backspace
            if (e.KeyValue == 32 || e.KeyValue == 'J' || e.KeyValue == 'P' ||
                (e.KeyValue == 'H' && (cGL.Robot.WeaponIndex == 3 || cGL.Robot.WeaponIndex == 2 || cGL.Robot.WeaponIndex == 1)))
            //when moving on ground,dont idle
            {
                cGL.Robot.Jump = 0;
                flagForAnotherStep = false;
                cGL.Robot.ShoulderLeftAngle = 0;
                cGL.Robot.ShoulderRightAngle = 0;
                cGL.Robot.LegLeftUpAngle = 0;
                cGL.Robot.LegRightUpAngle = 0;
                cGL.Robot.IsMoving = false;
                animationCount = 0;
                timer2.Enabled = false;
                cGL.Robot.IsJumping = false;
                cGL.Robot.IsDoingSalta = false;
                animationTime = 33;
                cGL.JumpSpeed = 1;
                cGL.Robot.Angle = 0;
                cGL.Robot.AngleCrash = 0;
                cGL.Move = 0.7f;
                cGL.Robot.LegLeftDownAngle = cGL.Robot.LegRightDownAngle = 0;
                cGL.Robot.IsCrashWithCar = false;
            }
            switch (e.KeyValue)
            {
                case 'w':
                case 'W':
                    timer2.Enabled = true;
                    cGL.Robot.IsMoving = true;
                    cGL.button = cOGL.arrow.forward;
                    break;
                case 's':
                case 'S':
                    timer2.Enabled = true;
                    cGL.Robot.IsMoving = true;
                    cGL.button = cOGL.arrow.backward;
                    break;
                case 'a':
                case 'A':
                    timer2.Enabled = true;
                    cGL.Robot.IsMoving = true;
                    cGL.button = cOGL.arrow.left;
                    break;
                case 'd':
                case 'D':
                    timer2.Enabled = true;
                    cGL.Robot.IsMoving = true;
                    cGL.button = cOGL.arrow.right;
                    break;
                case 32: //Space
                    timer2.Enabled = true;
                    cGL.Robot.IsMoving = true;
                    cGL.Robot.IsJumping = true;
                    break;
                case 'J':
                    timer2.Enabled = true;
                    cGL.Robot.IsMoving = true;
                    cGL.Robot.IsJumping = true;
                    cGL.Robot.IsDoingSalta = true;
                    break;
                case 'P':
                    IsPowerJump = !IsPowerJump;
                    break;
                case 'H':
                    switch (cGL.Robot.WeaponIndex)
                    {
                        case 1:
                        case 2:
                            timer4.Enabled = true;
                            cGL.Robot.IsJumping = true;
                            break;
                        case 3:
                            cGL.Robot.IsMoving = true;
                            timer3.Enabled = true;
                            cGL.Robot.IsJumping = true;
                            break;
                    }
                    break;
            }
        }

        #region Timers

        //this timer refresh the game picture every 10ms
        private void timer1_Tick(object sender, EventArgs e)
        {

            //handle when crash occured
            if (cGL.Robot.IsCrashWithCar && !crashFlagForTimer)
            {

                animationCount = 0;  //clear counter of walking animation for Crashing animation

                //init Robot state
                cGL.Robot.Jump = 0;
                cGL.Robot.ShoulderLeftAngle = 0;
                cGL.Robot.ShoulderRightAngle = 0;
                cGL.Robot.LegLeftUpAngle = 0;
                cGL.Robot.LegRightUpAngle = 0;
                cGL.Robot.Angle = 0;
                cGL.Robot.AngleCrash = 0;
                cGL.Robot.LegLeftDownAngle = cGL.Robot.LegRightDownAngle = 0;

                cGL.Robot.IsJumping = true;
                flagForAnotherStep = false;

                crashFlagForTimer = true;  //Enter one time
                timer2.Enabled = true;
            }

            cGL.Draw();

        }

        private void timer2_Tick(object sender, EventArgs e)   //for animation of walking,jumping,Crashing
        {
           
                animationCount++;
                if (animationCount < animationTime)
                {

                    cGL.Move = 0.7f;    //How much to Move
                    if (cGL.Robot.IsJumping || cGL.Robot.IsCrashWithCar)     //if jumping or Crashing
                    {
                        if (IsPowerJump)    //if pressed 'p',jump is stronger else standard
                        {
                            cGL.Move = 1.7f;
                            animationTime = 65;
                        }
                        else
                        {
                            cGL.Move = 0.7f;
                            animationTime = 33;
                        }
                        if (animationCount < animationTime / 2) //Half animation
                        {
                            cGL.JumpSpeed = cGL.JumpSpeed - 1 / (animationTime / 2);    //How fast and strong a jump
                            cGL.Robot.Jump += cGL.JumpSpeed;
                            if (cGL.Robot.IsDoingSalta) //if pressed 'j' jump Salta
                                cGL.Robot.Angle -= 11.25f;
                            else if (cGL.Robot.IsCrashWithCar) // Crash animation.Slow speed timer and camera Move
                            {
                                timer2.Interval = 70;
                                timer1.Interval = 70;
                                cGL.Robot.AngleCrash -= 11.25f;
                                cGL.RotY += 5;
                                cGL.Robot.WalkAngle = cGL.RotY;
                            }
                        }
                        else //Second half of animation
                        {
                            cGL.JumpSpeed = cGL.JumpSpeed + 1 / (animationTime / 2);
                            cGL.Robot.Jump -= cGL.JumpSpeed;
                            if (cGL.Robot.IsDoingSalta)
                                cGL.Robot.Angle -= 11.25f;
                            else if (cGL.Robot.IsCrashWithCar)
                            {
                                cGL.Robot.AngleCrash -= 11.25f;
                                cGL.RotY += 10;
                                cGL.Robot.WalkAngle = cGL.RotY;
                            }
                        }

                        if (cGL.Robot.Jump <= 0) //To stop jumping
                        {
                            cGL.Robot.IsJumping = false;
                            cGL.Robot.Jump = 0;
                            cGL.Robot.IsDoingSalta = false;
                        }

                    }


                    cGL.Move = 1;
                    if (flagForAnotherStep == false)  //Animation Move one step
                    {
                        cGL.Robot.ShoulderLeftAngle -= walkSpeed;
                        cGL.Robot.ShoulderRightAngle -= walkSpeed;
                        cGL.Robot.LegLeftUpAngle += walkSpeed;
                        cGL.Robot.LegRightUpAngle -= walkSpeed;

                        if (animationCount % 33 > (animationTime % 34) / 2) //To Move down legs correctly
                        {
                            cGL.Robot.LegLeftDownAngle += walkSpeed / 2;
                            cGL.Robot.LegRightDownAngle += walkSpeed / 2;
                        }
                        else
                        {
                            cGL.Robot.LegLeftDownAngle -= walkSpeed / 2;
                            cGL.Robot.LegRightDownAngle -= walkSpeed / 2;
                        }

                        if (cGL.Robot.LegLeftUpAngle > 30) //change one step to another
                            flagForAnotherStep = true;
                    }
                    else     //Animation Move another step
                    {
                        cGL.Robot.ShoulderLeftAngle += walkSpeed;
                        cGL.Robot.ShoulderRightAngle += walkSpeed;
                        cGL.Robot.LegLeftUpAngle -= walkSpeed;
                        cGL.Robot.LegRightUpAngle += walkSpeed;

                        if (animationCount % 33 > (animationTime % 34) / 2) //To Move down legs correctly
                        {
                            cGL.Robot.LegLeftDownAngle -= walkSpeed / 2;
                            cGL.Robot.LegRightDownAngle -= walkSpeed / 2;
                        }
                        else
                        {
                            cGL.Robot.LegLeftDownAngle += walkSpeed / 2;
                            cGL.Robot.LegRightDownAngle += walkSpeed / 2;
                        }

                        if (cGL.Robot.LegLeftUpAngle < -30)
                            flagForAnotherStep = false;
                    }
                }
                else  //return to idle state when finish animation
                {
                    timer1.Interval = 1;
                    timer2.Interval = 1;
                    timer2.Enabled = false;

                    animationCount = 0;

                    cGL.Robot.IsMoving = false;
                    cGL.Robot.IsJumping = false;
                    cGL.Robot.IsDoingSalta = false;
                    cGL.Robot.IsCrashWithCar = false;
                    crashFlagForTimer = false;

                    animationTime = 33;
                    cGL.JumpSpeed = 1;
                    cGL.Robot.Angle = 0;
                    cGL.Robot.AngleCrash = 0;
                    cGL.Move = 0.7f;
                    cGL.Robot.LegLeftDownAngle = cGL.Robot.LegRightDownAngle = 0;

                }
                cGL.Robot.Draw();
                cGL.Robot.DrawShadow();
            
         
        }

        private void timer3_Tick(object sender, EventArgs e) //for animation of ATTACK with sword
        {

            animationCountOfSwordAttack++;
            if (animationCountOfSwordAttack < animationTime)
            {
                cGL.Move = 0.7f;//How much to Move

                if (IsPowerJump)//if pressed 'p',jump is stronger else standart
                {
                    cGL.Move = 1.7f;
                    animationTime = 65;
                }
                else
                {
                    cGL.Move = 1.7f;
                    animationTime = 33;
                }
                if (animationCount < animationTime / 2) //Half animation
                {
                    cGL.JumpSpeed = cGL.JumpSpeed - 1 / (animationTime / 2);//How fast and strong a jump
                    cGL.Robot.Jump += cGL.JumpSpeed;
                    cGL.Robot.AngleWeapon3 -= 22.5f;// 11.25f;                       
                }
                else //Second half of animation
                {
                    cGL.JumpSpeed = cGL.JumpSpeed + 1 / (animationTime / 2);
                    cGL.Robot.Jump -= cGL.JumpSpeed;
                    cGL.Robot.AngleWeapon3 -= 22.5f;// 11.25f;       
                }
                if (cGL.Robot.Jump <= 0) //To stop jumping
                {
                    cGL.Robot.AngleWeapon3 = 0;
                    cGL.Robot.IsJumping = false;
                    cGL.Robot.Jump = 0;
                }
            }
            else  //return to idle state when finish animation
            {
                timer1.Interval = 1;
                timer2.Interval = 1;
                timer3.Enabled = false;

                animationCountOfSwordAttack = 0;

                cGL.Robot.IsMoving = false;
                cGL.Robot.IsJumping = false;
                cGL.Robot.IsDoingSalta = false;
                cGL.Robot.IsCrashWithCar = false;

                animationTime = 33;
                cGL.JumpSpeed = 1;
                cGL.Robot.Angle = 0;
                cGL.Move = 0.7f;
                cGL.Robot.LegLeftDownAngle = cGL.Robot.LegRightDownAngle = 0;

            }
            cGL.Robot.Draw();
            cGL.Robot.DrawShadow();
        }


        private void timer4_Tick(object sender, EventArgs e)  //for animation of SHOOTING guns
        {
            if (cGL.Robot.ShootDist < 80)
            {
                if (cGL.Robot.WeaponIndex == 2)    //gun2
                {
                    cGL.Robot.ShootDist += 10;
                    if (cGL.Robot.ShootDist < 40)
                        cGL.Robot.WeaponPlace -= 8;
                    else
                        cGL.Robot.WeaponPlace += 8;
                }
                else  //gun1
                {
                    if (cGL.Robot.WeaponPlace == 0)
                        cGL.Robot.WeaponPlace = 27;
                    cGL.Robot.ShootDist += 3;
                    cGL.Robot.WeaponPlace -= 1;
                }
            }
            else
            {
                cGL.Robot.ShootDist = 0;
                cGL.Robot.WeaponPlace = 0;
                cGL.Robot.IsJumping = false;
                timer4.Enabled = false;
            }
            cGL.Robot.Draw();
            cGL.Robot.DrawShadow();
        }

        #endregion

        #region RadioButtonsCheckedChanged

        private void radioButton0_CheckedChanged(object sender, EventArgs e)
        {
            cGL.Robot.WeaponIndex = 0;
            cGL.Weaponchanged = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cGL.Robot.WeaponIndex = 1;
            cGL.Weaponchanged = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            cGL.Robot.WeaponIndex = 2;
            cGL.Weaponchanged = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            cGL.Robot.WeaponIndex = 3;
            cGL.Weaponchanged = true;
        }

        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

    }
}