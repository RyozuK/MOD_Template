
# Elin Mod Template

This project serves as a template for BepInEx mods for Elin, as well as a quickstart guide for first time modders.

It assumes you have at least a baseline understanding of programming concepts.  A full programming tutorial is outside the scope of this document.  Also keep in mind, not every mod requires scripting.  Through the use of various no-script solutions such as CustomWhateverLoader, you can create content mods without the need to worry about script templates such as this one.  It's also assumed that you are working in Windows, since this is a Windows game.

## Getting Started: One time tasks

### Software to install
  * **JetBrains Rider**: A powerful IDE for .NET development. It is free for Noncommercial use.  There are a [variety of installation methods, start here](https://www.jetbrains.com/help/rider/Installation_guide.html)
  * **Optional: Git CLI**: Useful for command-line interaction with your Git repositories.  You can [find more information here](https://github.com/git-guides/install-git)

### Accounts to make
  * **GitHub**: Create a GitHub account if you don't already have one. GitHub allows you to host your mod project, collaborate with others, and keep track of your changes using version control.

### Initial setup
  * **Use GitHub Template**: Start by using this template to copy the project repository to your own GitHub account. This will serve as the base for your mod development. Look for a big green button in the top right of this repository's page labeled ``Use This Template``
    * Now is a good time to do a little planning ahead, come up with a unique and descriptive name for your repository that reflects the kind of mod you plan to make.
  * **Clone project to local drive**: Once you've created the project, you can copy the files to your local hard drive using the git command line interface.  Find the command by clicking "<>Code" and copying the URL.  If you've installed the git cli, you can then do the following in a terminal window 
    ```sh
    git clone https://github.com/YourUserName/MOD_YourModName.git
    ``` 
    Replace the URL as appropriate.  git will create a folder based on your project name.  Open this folder in JetBrains Rider.  IMPORTANT: Do not simply open individual files.  The folder as a whole is your project folder.
    
  * Alternatively, you can open the project in Rider by creating a new project from GitHub.
    * Set up Rider with GitHub account: Integrate your GitHub account into Rider for seamless repository management.

  * If you really, truly, don't want to do any of that, you can simply download a ZIP of the template repository and unzip it.  I strongly suggest installing git and cloning it properly however: Version control saves sanity.

### Enable Logging
Let's turn on the Unity/BepInEx console, if we haven't already.  This will be extremely helpful for solving issues when debugging your mod later.
  * First, in your Steam Library, select Elin, click the Gear icon->"Manage"->"Browse Local Files".
  * Note this folder path for later, you'll need this information for later.
  * Open the ``BepInEx\Config`` folder.  Inside there, open ``BepInEx.cfg``
  * Around line 38, change ``Enabled = false`` to ``Enabled = true``.

When all is said and done, you should have a folder containing the template, and have it opened in Rider.  Rider is likely saying there are errors at this point, we'll address those in a moment.

## Project details: Tasks to perform with each new mod
Before you begin modding, you'll need to make some initial changes to the provided files.  To start, let's make sure we understand that a project folder and a mod folder are separate things.  The project folder contains all the source code, resources, and information for the project, much of which isn't needed for the mod itself.  The mod folder is a separate folder that the game itself looks at to load information for the mod.  As such, placing the project folder itself into the game directory is a bad idea.

When working on your mod, each time you build the mod, the provided build script will compile your source code into a DLL, copy the DLL to a folder in the game directory, as well as anything in the ``resources`` folder.  Elin will treat this folder as a mod, and within the game's mod browser, it will be labeled with a ``[Local]`` tag.  If any users install your mod via the workshop, it will be placed in a workshop provided folder somewhere else, so be careful when loading resources not to expect the files to be located in a specific folder within the game's folder itself. The folder containing your mod, after a build, will be something like ``Elin\Package\MOD_YourMod``

### Explanation of resources folder
  * **All contents are copied with each build**: Files in the resources folder will be copied each time you build your mod.
  * **Mod folder is cleaned with each build**: The mod folder is cleared before each new build, ensuring that any old files are removed.

If your mod's resources folder only consists of a ``package.xml`` and ``preview.jpg`` that's fine, but keep in mind those two files bare minimum.

### Editing sources
  * **Edit package.xml / preview.jpg inside the resources folder**: Customize these files to reflect the specific details of your mod, such as metadata and a preview image. You can find [more information here as well](https://elin-modding-resources.github.io/Elin.Docs/articles/2_Getting%20Started/basic_mod#writing-package-xml).
    * First is the mod's title.  This is what will appear on the workshop.
    * Next is the mod's ID.  This should be something unique and descriptive. If you're at a loss what to use, a common convention is to use the URL of your GitHub project.  For example, ``com.github.ryozuk.MOD_Template``. The important thing is that this ID shouldn't match anyone else's id.
    * The author's name or pseudonym should be self-explanatory.
    * Load priority determines what order mods should be loaded in.  For the most part, this shouldn't matter much.
    * Description is what the initial description of your mod will be when you publish to the Steam Workshop the first time.  Don't worry, you can edit it later.
    * Tags are used for Workshop organization, [there's an official list here](https://docs.google.com/document/u/2/d/e/2PACX-1vR7MjQ_5hAmavFB8iMW6xm7vSYJg_g8I1s8KtvjBO-N_zNATnsmdmyQsmxQ8z9yEpZxNoc-TTdZm8so/pub)
    * Version is the last version of the game itself that your mod was tested on.  Elin will avoid loading your mod on any version earlier than the one you sepcify here.
    * Builtin should be left as false
    * Visibility on the workshop will be set to this with each publish.  You may want to change this to "public" once you get the hang of things. 
  * **Rename MOD_Template.csproj**: Rename this file to reflect the name of your mod.
  * **Edit csproj**: Open your newly renamed csproj file, you'll need to make a few changes here. 
    * On line 5, change  this to your mod's name.  This will be the name of the folder it's published to, so no illegal characters.
    * On line 22, you'll see a path to Elin, except it's pointing to the F drive.  Change this to your game's path.
  * **Rename and Edit MOD_TemplatePlugin.cs**: This is your mod's main file.  You can create more files, but this is the one that will be the core of your mod.
    * When renaming source files such as this, it's best to do so inside of Rider by selecting ``Refactor->Rename`` in the file's context menu.  Rename this file to match your mod's name/id.  You'll note the class name changes as well as a few other things.  Such is the power of refactor.
    * At the top of this file is a class called ``ModInfo``.  Go ahead and change the values in here as appropriate.

## Explanation of provided code
This template comes with a few handy bits built in already.
  * **Loggers**: The template includes logging methods to help you keep track of important events and debug issues.  LogInfo and LogDev will print their messages in both the Player.log and the Unity/BepInEx console.  These log functions use reflection and/or compiler features to help you keep track of which messages are printed from which section of code.
    * ``! Reflection can be slow and expensive, be sure to change DevMode to false before publishing !``
  * **Example postfix**: A postfix patch example is provided to show how you can modify or extend game behavior.  There are a variety of ways to utilize HarmonyLib to change and affect behavior, more details can be found on the [Elin Modding wiki](https://elin-modding-resources.github.io/Elin.Docs/)
  * **Example console command**: This example demonstrates how to add custom console commands to your mod for additional functionality.
    * Instead of creating an object that performs some action needed only for testing, you can add a command to be used in the in-game console.

Feel free to remove any of these features if they are not appropriate for your mod.

* Publishing
  * **Where to publish inside Elin**:
    * If you're satisfied with your mod and ready to publish it, you can find it within Elin itself in the mod viewer.  Clicking the mod should allow you to publish it.
  * **Each publish will contain only the mod folder's files**: Ensure only the necessary files are included in your build.
  * **Set dependencies if used**: On your mod's steam workshop page, be sure to set any dependencies your mod uses.
  * **It's up to you to update the version/change log**: Track changes and updates to your mod using a change log.  By default, "Initial Release" will always be put into the change log with each publish.
  * **Other best practices for Steam mod page**
    * Consider creating and pinning discussion topics for bug reports, feature suggestions, or other feedback.
    * This game has an international userbase.  While the majority of players are Japanese, there are also Chinese and French players.  Consider localization for those languages as well.
    * Upload some preview images and update the description!  A good mod preview entices users to check it out.
