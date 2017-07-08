using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hide_and_Seek
{
    public partial class Form1 : Form
    {
        int Moves;

        Location currentLocation;

        RoomWithDoor livingRoom;
        RoomWithHidingPlaces diningRoom;
        RoomWithDoor kitchen;
        Room stairs;
        Room basementStairs;
        RoomWithHidingPlaces hallway;
        RoomWithHidingPlaces bathroom;
        RoomWithHidingPlaces masterBedroom;
        RoomWithHidingPlaces secondBedroom;

        OutsideWithDoor frontYard;
        OutsideWithDoor backYard;
        OutsideWithHidingPlaces garden;
        OutsideWithHidingPlaces driveway;

        Opponent opponent;

        public Form1()
        {
            InitializeComponent();
            CreateObjects();
            opponent = new Opponent(frontYard);
            ResetGame(false);
        }

        private void MoveToANewLocation(Location newLocation)
        {
            Moves++;
            currentLocation = newLocation;
            RedrawForm();
        }

        private void RedrawForm()
        {
            exits.Items.Clear();
            for (int i = 0; i < currentLocation.Exits.Length; i++)
                exits.Items.Add(currentLocation.Exits[i].Name);
            exits.SelectedIndex = 0;
            description.Text = currentLocation.Description + "\r\n(move #" + Moves + ")";
            if (currentLocation is IHidingPlaces)
            {
                IHidingPlaces hidingPlace = currentLocation as IHidingPlaces;
                check.Text = "Check " + hidingPlace.HidingPlaceName;
                check.Visible = true;
            }
            else
                check.Visible = false;
            if (currentLocation is IHasExteriordoor)
                goThroughTheDoor.Visible = true;
            else
                goThroughTheDoor.Visible = false;
        }

        private void CreateObjects()
        {
            livingRoom = new RoomWithDoor("Living Room", "an antique carpet", "inside the closet", "an oak door with a brass handle");
            diningRoom = new RoomWithHidingPlaces("Dining Room", "a crystal chandelier", "in the tall armoire");
            kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances", "in the cabinet", "a screen door");
            stairs = new Room("Stairs", "a wooden bannister");
            hallway = new RoomWithHidingPlaces("Upstairs Hallway", "a picture of a dog", "in the closet");
            bathroom = new RoomWithHidingPlaces("Bathroom", "a sink and a toilet", "in the shower");
            masterBedroom = new RoomWithHidingPlaces("Master Bedroom", "king size bed", "under the bed");
            secondBedroom = new RoomWithHidingPlaces("Second Bedroom", "single size bed", "under the bed");
            frontYard = new OutsideWithDoor("Front Yard", false, "a heavy-looking oak door");
            backYard = new OutsideWithDoor("Back Yard", true, "a screen door");
            garden = new OutsideWithHidingPlaces("Garden", false, "inside the shed");
            driveway = new OutsideWithHidingPlaces("Driveway", true, "in the garage");
            basementStairs = new Room("Stairs to the basement", "a picture of a fruit basket");

            diningRoom.Exits = new Location[] { livingRoom, kitchen };
            livingRoom.Exits = new Location[] { diningRoom, stairs, basementStairs };
            kitchen.Exits = new Location[] { diningRoom };
            stairs.Exits = new Location[] { livingRoom, hallway, basementStairs };
            hallway.Exits = new Location[] { stairs, basementStairs, bathroom, masterBedroom, secondBedroom };
            bathroom.Exits = new Location[] { hallway };
            masterBedroom.Exits = new Location[] { hallway };
            secondBedroom.Exits = new Location[] { hallway };
            frontYard.Exits = new Location[] { backYard, garden, driveway };
            backYard.Exits = new Location[] { frontYard, garden, driveway };
            garden.Exits = new Location[] { backYard, frontYard };
            driveway.Exits = new Location[] { backYard, frontYard };
            basementStairs.Exits = new Location[] {livingRoom, stairs, hallway };

            livingRoom.DoorLocation = frontYard;
            frontYard.DoorLocation = livingRoom;

            kitchen.DoorLocation = backYard;
            backYard.DoorLocation = kitchen;
        }

        private void ResetGame(bool displayMessage)
        {
            if (displayMessage)
            {
                MessageBox.Show("You found me in " + Moves + " moves!");
                IHidingPlaces foundLocation = currentLocation as IHidingPlaces;
                description.Text = "You found your opponent in " + Moves + " moves! He was hiding " + foundLocation.HidingPlaceName + ".";
            }
            Moves = 0;
            hide.Visible = true;
            gohere.Visible = false;
            check.Visible = false;
            goThroughTheDoor.Visible = false;
            exits.Visible = false;
        }

        private void gohere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currentLocation.Exits[exits.SelectedIndex]);
        }

        private void goThroughTheDoor_Click(object sender, EventArgs e)
        {
            IHasExteriordoor hasDoor = currentLocation as IHasExteriordoor;
            MoveToANewLocation(hasDoor.DoorLocation);
        }

        private void check_Click(object sender, EventArgs e)
        {
            Moves++;
            if (opponent.Check(currentLocation))
                ResetGame(true);
            else
            {
                MessageBox.Show("No one in there!");
                RedrawForm();
            }
        }

        private void hide_Click(object sender, EventArgs e)
        {
            hide.Visible = false;

            for (int i = 1; i <= 10; i++)
            {
                opponent.Move();
                description.Text = i + "... ";
                Application.DoEvents();
                System.Threading.Thread.Sleep(300);
            }

            description.Text = "Ready or not, here I come!";
            Application.DoEvents();
            System.Threading.Thread.Sleep(1000);

            gohere.Visible = true;
            exits.Visible = true;
            MoveToANewLocation(livingRoom);
        }
    }
}