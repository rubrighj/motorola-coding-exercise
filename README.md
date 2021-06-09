# motorola-coding-exercise
This code sample meets the following criteria:
 1. Loads the dates.txt file, reads each date one line at a time and if the date is valid will download the images. This use case is handled within the Unit Tests and the images,     if any are downloaded, are located at /NASA/NASA_Test/photos/<formatted earth date>.
 2. Language used is C#/.NET 5 with a jquery framework frontend.
 3. The project builds and runs locally using IIS Express. The Index.html page has been configured to load at project start/browser launch.
 4. All relevant documentation is included in this ReadMe for executing the application.
 5. For the application to run correctly you will need to do a Find-Replace All for the 4 instances of the string <DEMO_KEY>. For security purposes I am not including my API Key.     If it is required then I will email it to each individual performing the code exercise review.
 6. For additional information/bonus I have included a simple set of unit tests. Additionally, a user can enter a date, choose a camera, and enter a page number and search for         images, which are displayed on the Index.html page. While testing the date Jul-13-2016, 392 images were located and even with a Parallel.ForEach call to try and download as       many images as possible this call still takes about 10-20 seconds to download, save and then display so please be a little patient when running that test in the browser.
