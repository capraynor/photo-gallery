import { MediaDirectory } from "./models/MediaFile";
import { get, put } from "./request";

export async function getAllMediaDirectories(){
    var result = await get<MediaDirectory[]>("/api/media-directories");
    return result;
}

export async function addMediaDirectory(directoryPath: string){
    var md = new MediaDirectory();
    md.path = directoryPath;
    var result = await put<MediaDirectory, MediaDirectory>("/api/media-directories", md);
    return result;
}

