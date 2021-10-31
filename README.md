# Project Zero 5 FMSG Extractor

A program used to extract/re-import text files of the Fatal Frame 5: Maiden of Black Water (PC).

## Usage:
* Type "-e [Input File/Folder] [Output File/Folder]" to extract the file/folder.
* Type "-i [Original File/Folder] [Input File/Folder] [Output File/Folder]" to re-import the file/folder.

## Example:
```

//Extract
PJ5-FMSG-Extractor.exe -e digital_artbook.fmsg digital_artbook.fmsg.txt //for file
PJ5-FMSG-Extractor.exe -e USen USen-extracted //for folder

//Import
PJ5-FMSG-Extractor.exe -i digital_artbook.fmsg digital_artbook.fmsg.txt digital_artbook.fmsg.new //for file
PJ5-FMSG-Extractor.exe -i USen USen-extracted USen-new //for folder

```