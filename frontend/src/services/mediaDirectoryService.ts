import { MediaDirectory } from "./models/MediaFile";
import { get, post, put } from "./request";

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

export async function startScanDirectory(directoryId: string){
    var result = await post<unknown, MediaDirectory>(`/api/media-directories/scan-directory?directoryId=${directoryId}`, null);
    return result;
}
