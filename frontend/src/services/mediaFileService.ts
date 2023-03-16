import { MediaFile } from "./models/MediaFile";
import { BaseURL, get } from "./request";
import urlJoin from "url-join";


var totalCount = 0;
const mediaFiles: MediaFile[] = [];

export async function initializeTotalCount(){
  const count = await getTotalFileCount();
  totalCount = count;
}

export function getMediaFileByCountSync(mediaFileCount: number){
  const result =  mediaFiles[mediaFileCount];
  return result;
}

export async function getTotalFileCount(): Promise<number>{
  const result = await get<number>("/api/media-files/count");
  return result;
}

export async function getMediaFiles(from: number, to: number): Promise<MediaFile[]>{
  let startFileNumber = from;
  let endFileNumber = to;

  if (startFileNumber > totalCount){
    return [];
  }
  if (endFileNumber > (totalCount - 1)){
    endFileNumber = totalCount;
  }
  var mediaFiles = await get<MediaFile[]>(`/api/media-files?skip=${startFileNumber}&take=${endFileNumber - startFileNumber + 1}`)

  let fileNumber = startFileNumber;
  mediaFiles.forEach(x => {
    x.displayCount = fileNumber;
    x.requestPath = urlJoin(BaseURL, x.requestPath);
    mediaFiles[fileNumber] = x;
    fileNumber ++;
  });

  return mediaFiles;
}
