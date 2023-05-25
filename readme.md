# Photo Gallery
Viewing photos on NAS is kind of hard job. This project indends to design a photo viewing tool, with the foollowing characteristics: 

- Viewing photos / videos using browser - no need to download any applications. 
- Viewing photos / videos ordered by time.
- Viewing all photos / videos in subdirectories. 
- Support large quantity of photos / videos (more than 3,000,000)
- Show EXIF data when viewing a photo. 
- Show photos on map (read EXIF location data, and mark photo on map)
- Provide a scrollbar with time and photo preview. The scrollbar's responding speed should be fast. 
- Support iPhone live photos (read the uploaded result of [PhotoSync](https://www.photosync-app.com/home))
- Allow group media files 

Also, this project is my personal practicing project, aim to finding a correct way to using .Net Core MVC, and implementing a frontend timeline (load on demand when scrolling)

- Found a correct way to write UT, database connection string inside `appsettings.json` no longer is a blocker of UT. See: [Configure.cs](Photo%20Gallery/Photo%20Gallery/Configure.cs) (Application configuration) and [PhotoGalleryUnitTest.cs](Photo%20Gallery/UnitTests/PhotoGalleryUnitTest.cs) (Unit test base class)
- Implemented background jobs using `BackgroundService` and `Blocking Collection` (Instead of any third-party message queue services, which is not sutible for a poor NAS CPU and RAM capacity). 
- Implemented a timeline using TypeScript and CSS [Timeline.tsx](frontend/src/controls/Timeline.tsx)
    - Supports load on demand (while scrolling)
    - Has a simple diff logic for better performance. 