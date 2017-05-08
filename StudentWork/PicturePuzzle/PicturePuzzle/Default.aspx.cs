using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    Puzzle thePuzzle;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ImageButton_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["Array"] == null)
            thePuzzle = new Puzzle();   // make a puzzle object for the first time
        else
            thePuzzle = new Puzzle((int[])Session["Array"]);    // make a puzzle object from an existing picture array

        // process clicks for swapping an image
        ImageButton btnClicked = (ImageButton)sender;
        if (Session["TempURL"] == null)     // first click
        {
            Session["TempURL"] = btnClicked.ImageUrl;
            Session["SourceID"] = btnClicked.ID;
            btnClicked.ImageUrl = "";
        }
        else                               // second click
        {
            if (btnClicked.ID == (string)Session["SourceID"])  // put the image back in the same place
            {
                btnClicked.ImageUrl = (string)Session["TempURL"];
            }
            else                               // swap images
            {
                ImageButton btnSource = (ImageButton)FindControl((string)Session["SourceID"]);
                btnSource.ImageUrl = btnClicked.ImageUrl;
                btnClicked.ImageUrl = (string)Session["TempURL"];

                // update the picture positions in the puzzle object
                thePuzzle.move(((string)Session["SourceID"]).Substring(11), btnClicked.ID.Substring(11));
                Session["Array"] = thePuzzle.Pictures;

                // check to see if the puzzle is done
                if (thePuzzle.isFinished())
                    lblFinished.Text = "Congratulations!";
                else
                    lblFinished.Text = "Not done yet";
            }
            Session["TempURL"] = null;          // so we'll know the second click is done
        }
    }
}