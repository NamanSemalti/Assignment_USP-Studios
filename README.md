# Assignment_USP-Studios
 

 Project Architecture

 
UIManager: Manages user input and updates the interface based on parsed word data.

FlowManager: Central hub for dispatching and managing events.

WordDataLoader: Handles the API call, response parsing, and audio playback.

Helper_UnityWebRequest: Handles asynchronous HTTP requests and audio clip downloads.

AudioManager: Plays audio and manages character state accordingly.

ScreenManager: Shows/hides loading and error screens.

SingletonMonoBehaviour<T>: Ensures classes like FlowManager, ScreenManager, etc., are treated as singletons.

StaticDataHolder: Central store for static strings and animator keys.

HelperFunctions: Utility for simple string validation.

Data Models: WordData, Phonetic, Meaning, Definition represent structured data from the dictionary API.

Rotator: Utility MonoBehaviour that rotates objects (possibly for UI or visual feedback).