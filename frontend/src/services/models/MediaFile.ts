export class MediaFile {
  public filePath: string;
  public thumbnailFilePath: string;
  public shottingDate: string;
  public createdDate: string;
  public longitude: number;
  public latitude: number;
  public mD5Str: string;
  public id: string;
  public mediaDirectoryId: string;
  public requestPath: string;
  public displayCount: number;
  public fileType: MediaFileType;

  constructor(){
    
  }
}

export class MediaDirectory{
  public path: string;
  public id?: string;
  public photosCount?: string;
}

export enum MediaFileType{
  Image = 0,
  Video = 1,
  Unknown = 2
}