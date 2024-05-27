# HolyWater test task

This test task involves creating a simple game with two distinct scenes, designed to
evaluate your ability to work with dynamic content, user interactions, and data persistence in
Unity. The first scene serves as an introductory interface with basic elements including a
background, logo, progress bar, and a button to load the main scene. The main scene presents
more complex functionalities such as displaying information in a modal window, navigating
through a set of interactive elements, and managing a collection of weather cards. A key
requirement is ensuring that the game preserves the state across sessions, displaying the last
known state upon re-entry. The game should be optimized for portrait mode display on various
screen sizes and developed using Unity 2020.3 LTS or later. Creativity is encouraged for any
aspects not specifically outlined in this task.

# First Scene Configuration:
Background and Elements:
The scene should feature a distinctive background, a prominently displayed logo, and a
“Load Second Scene” button. Feel free to select any design for these elements according to
your preference.
Include a circular progress bar that visually indicates the loading progress of the second scene
once the “Load Second Scene” button is clicked.

# Second Scene Configuration:
Introduction Window:
Upon entering the second scene, display a modal window with a semi-transparent
backdrop showcasing the following details: the title of the test assignment, the author's name,
and a clickable link to one of the author's social media profiles.
The window should feature a close button (represented by a cross sign) in its upper right corner,
allowing the window to be closed.
Ensure the background area behind the modal window is non-interactive while the window is
open.
Implement animations for opening and closing the window, focusing on a smooth transition in
transparency.
This window should be accessible from the game's settings via a designated button.
# Main Scene Layout:
Header: Fixed at the top, contains:
An "Exit" button on the left to terminate the game.
A settings button on the right, opening a settings window with:
- Toggles for music and sound effects, both enabled by default. Adjusting music should
smoothly transition the volume.
- A button to reopen the introduction window.
- A close button for the settings window.
# Horizontal Scroll Panel:
Positioned below the header, it should:
Contain three interactive objects scrolling automatically from left to right and vice versa, looping
back around.
Allow users to manually swipe through the objects. The panel should auto-align to the object
nearest to the center.
# Vertical Scroll Bar (Weather Cards):
Below the scroll panel, featuring:
Nine weather cards arranged in a grid of three columns, each displaying weather data for a
different location including the location name, an icon for the current weather condition, and the
current temperature.
Recommended APIs for weather data: OpenWeatherMap Current Weather and Weather
Conditions.
Cards should be removable by clicking, with subsequent cards in the column moving up to fill
the space.
# Footer: 
Contains a "Reset window" button at the bottom, resetting the scene to its
original state with all weather cards present.
# Final Requirements:
## Data Persistence:
All changes in the second scene must be saved as a JSON structure. Upon
re-entering the game, the window should display the state of the last exit, ensuring continuity for
the user.
## Display Compatibility:
The game must be designed exclusively for portrait mode and should
display correctly on portrait screens of any dimensions.
## Development Environment:
Use Unity 2020.3 LTS version or later for project development.
## Project Submission:
The completed project must be uploaded to a Git repository (GitHub,
GitLab, Bitbucket, etc., at the author's choice).
## Creative Liberty:
Details not explicitly mentioned in the test assignment are open for your
creative interpretation.
