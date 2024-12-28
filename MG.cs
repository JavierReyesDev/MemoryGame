namespace MemoryGame
{
    public partial class MG : Form
    {
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!", "!", "b", "b", "k", "k", "r", "r",
            "c", "c", "d" , "d" , "e" , "e", "h", "h",
        };
        Label firstClicked, secondClicked;
        private int gameTime = 0;

        /// <summary>
        /// Initializes the game form and assigns icons to the labels.
        /// </summary>
        public MG()
        {
            InitializeComponent();
            AssignIcons();
        }

        /// <summary>
        /// Randomly assigns icons from the list to the labels in the table layout.
        /// </summary>
        private void AssignIcons()
        {
            Label label;
            int randomNumber;

            for (int i = 0; i < tbl_layout.Controls.Count; i++)
            {
                // Check if the current control is a Label.
                if (tbl_layout.Controls[i] is Label)
                {
                    label = (Label)tbl_layout.Controls[i];
                }
                else
                {
                    continue;
                }

                randomNumber = random.Next(0, icons.Count);

                // Assign an icon to the label from the list.
                label.Text = icons[randomNumber];

                // Remove the assigned icon to avoid duplicates.
                icons.RemoveAt(randomNumber);
            }

            timer2.Start();
        }

        /// <summary>
        /// Handles label clicks, managing the game logic for revealing icons and matching pairs.
        /// </summary>
        private void lbl_Click(object sender, EventArgs e)
        {
            // Prevent interaction if two labels are already clicked.
            if (firstClicked != null && secondClicked != null) return;

            // Ensure the clicked object is a Label.
            Label clickedLabel = sender as Label;
            if (clickedLabel == null) return;

            // Ignore clicks on already revealed labels.
            if (clickedLabel.ForeColor == Color.Chocolate) return;

            if (firstClicked == null)
            {
                // Store the first clicked label and reveal its icon.
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Chocolate;
                return;
            }

            // Store the second clicked label and reveal its icon.
            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Chocolate;

            // Check if the game is won.
            checkForWinner();

            if (firstClicked.Text == secondClicked.Text)
            {
                // Reset for the next pair if the icons match.
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                // Start a timer to hide unmatched icons after a delay.
                timer1.Start();
            }
        }

        /// <summary>
        /// Timer event to hide unmatched icons after a short delay.
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            // Hide the icons by resetting their color.
            firstClicked.ForeColor = Color.LemonChiffon;
            secondClicked.ForeColor = Color.LemonChiffon;

            // Reset the clicked labels for the next turn.
            firstClicked = null;
            secondClicked = null;
        }

        /// <summary>
        /// Checks if all pairs have been matched and the game is won.
        /// </summary>
        private void checkForWinner()
        {
            Label label;
            for (int i = 0; i < tbl_layout.Controls.Count; i++)
            {
                label = tbl_layout.Controls[i] as Label;

                // If any label still has its default color, the game is not finished.
                if (label != null && label.ForeColor == label.BackColor) return;
            }

            // Stop the game timer and display a victory message.
            timer2.Stop();
            MessageBox.Show("You won! You took " + gameTime + " seconds!");
        }

        /// <summary>
        /// Timer event to track the elapsed game time in seconds.
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            gameTime++;
        }
    }
}
