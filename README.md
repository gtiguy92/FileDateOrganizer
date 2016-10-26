# FileDateOrganizer
A console app which moves files into folders based on the file's date taken (for pictures) or date modified.

I built this app when Picasa stopped working for imports in Windows 10. The application will move files based on Date Taken or Date Modified from a source directory to a destination directory. The files will be placed in sub-directories in the destination directory. The sub-directories are named using the file date (Date Taken or Date Modifed) in the format "yyyy-DD-mm".

The app is run by providing a source directory parameter and a destination directory parameter. 

**Example:** `FileDateOrganizer.exe "C:\chris phone" "C:\Users\Chris\Pictures"`
