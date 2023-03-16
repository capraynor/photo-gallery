import { MediaFile } from "./models/MediaFile";
import { BaseURL, get } from "./request";
import urlJoin from "url-join";


var totalCount = 0;
const MEDIA_FILES_CACHE: MediaFile[] = [];

export async function initializeTotalCount(){
  const count = await getTotalFileCount();
  totalCount = count;
}

export function getMediaFileByCountSync(mediaFileCount: number){
  const result =  MEDIA_FILES_CACHE[mediaFileCount];
  return result;
}

export async function ensureMediaFileMetadata(mediaFileCount: number){
  const startCount = Math.min(0, mediaFileCount - 100);
  const endCount = Math.max(totalCount, mediaFileCount + 100);
  for (let i = startCount; i < endCount; i++){
    
  }
  getMediaFiles(startCount, endCount);

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
    MEDIA_FILES_CACHE[fileNumber] = x;
    fileNumber ++;
  });

  return mediaFiles;
}
