# BOBA SIMULATOR 2020

## Setting up Console Enhanced free
1. In Unity, click Window > Console Enhanced.
1. Drag the newly-opened window into whichever tab group your regular console is in.
1. Right click your regular console tab and click "Close tab".

## Updating Unity
1. Download and install latest Unity version from Unity Hub.
1. Find the project in the Unity Hub "Projects" tab and select the newly-installed version of Unity in the "Unity Version" dropdown.
1. Click the project, and when prompted to upgrade, click "CONFIRM".
1. Open up the project in Visual Studio and check for editor updates (Visual Studio > Check For Updates).
1. Back in Unity, open up the root scene (TitleScene) and play through the game in order to check for bugs and allow for recompilation.
1. Open a branch (`update-unity`).
1. Commit using this format:

        Upgrade Unity

        * 2019.3.3f1 -> 2019.3.4f1
1. Open a pull request with the following title format: `Upgrade Unity to 2019.3.4f1`.
1. Get your spouse to review it.

## Running tests
1. In Unity, Go to Window > General > Test Runner
1. Drag Test Runner tab to next to console tab (it won't persist between editor sessions FYI)
1. Click on either the EditMode tab for unit tests or PlayMode tab for integration tests
1. Click "Run All", "Run Selected", or "Rerun Failed"
