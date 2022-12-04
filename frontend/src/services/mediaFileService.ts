import { MediaFile } from "./models/MediaFile";

// export async function getMediaFiles() {
//   var fakeMediaFiles =
//     [
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\DSC01409.JPG",
//         "thumbnailFilePath": null as string,
//         "shottingDate": "2022-10-25T14:01:09.5516663+00:00",
//         "createdDate": "2022-10-25T14:01:09.5516663+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "3635ca0e3fed0eb47e9c892a22f838b8",
//         "id": "07cfa6f3-e65f-49c7-9a6e-77fa04128cb1",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\IMG_1077.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-06-26T10:21:57+00:00",
//         "createdDate": "2022-06-26T10:21:57+00:00",
//         "longitude": 108.9339370727539,
//         "latitude": 34.226566314697266,
//         "mD5Str": "63c8a4468a28eb15055af7763a1494cf",
//         "id": "b6974993-ccfe-40c4-865f-9787e0225ec7",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\IMG_1437.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-09-25T03:29:22+00:00",
//         "createdDate": "2022-09-25T03:29:22+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "79ca1a8c4a5bef4b5ee1119e6c8eb5f4",
//         "id": "2ce403c7-8ee9-46ca-8bfb-05d79b14e317",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\SubDir2\\DSC01369.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-10-25T14:01:06.0719436+00:00",
//         "createdDate": "2022-10-25T14:01:06.0719436+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "5859634e83a8d30325b9f5a8e452638b",
//         "id": "4c2dce02-42fa-4f7c-8226-3413462d8ac0",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\SubDir2\\DSC01370.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-10-25T14:01:41.233426+00:00",
//         "createdDate": "2022-10-25T14:01:41.233426+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "223e695f94e351f1097ef56bed52e6e6",
//         "id": "21cd0dd4-0c15-4bfb-95f9-04d0d315d074",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\SubDir2\\DSC01378.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-10-25T14:00:58.513917+00:00",
//         "createdDate": "2022-10-25T14:00:58.513917+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "c72b3991e525b15a20e6634462bf6860",
//         "id": "6e26063c-adfb-4b9d-8065-7fb77b14aa98",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\SubDir1\\SubDir1.1\\DSC01379.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-10-25T14:00:53.9052528+00:00",
//         "createdDate": "2022-10-25T14:00:53.9052528+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "c80dd6fe3194de34da43aa095df497b4",
//         "id": "606f6582-b045-49a1-8728-cfdcd2038e05",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\SubDir1\\SubDir1.1\\DSC01397.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-10-25T14:00:59.6249553+00:00",
//         "createdDate": "2022-10-25T14:00:59.6249553+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "f1539598466092fa0bfbcd2697f6baa2",
//         "id": "b1596889-9bf1-458c-b504-f9d527929a9a",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\SubDir1\\SubDir1.2\\DSC01379.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-10-25T14:00:53.9112622+00:00",
//         "createdDate": "2022-10-25T14:00:53.9112622+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "c80dd6fe3194de34da43aa095df497b4",
//         "id": "1d4f844c-dbae-49b3-a4b2-0d0966c5fe20",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\SubDir1\\SubDir1.2\\DSC01397.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-10-25T14:00:59.6299417+00:00",
//         "createdDate": "2022-10-25T14:00:59.6299417+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "f1539598466092fa0bfbcd2697f6baa2",
//         "id": "94852a5d-6c6d-4cdb-8d58-95ec3fa223df",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       },
//       {
//         "filePath": "C:/repositories/photo-gallery/Photo Gallery/UnitTests/TestData/Photos\\SubDir1\\SubDir1.2\\DSC01408.JPG",
//         "thumbnailFilePath": null,
//         "shottingDate": "2022-10-25T14:00:29.8959614+00:00",
//         "createdDate": "2022-10-25T14:00:29.8959614+00:00",
//         "longitude": 0.0,
//         "latitude": 0.0,
//         "mD5Str": "da3cc70933efa311fe830322389231f0",
//         "id": "ce0edca0-ee32-42d9-8dfa-3e27da7b9cb5",
//         "mediaDirectoryId": "c6e423a5-52bd-466d-99e8-9ea4a7ad8d4c"
//       }
//     ]
//     let fakedDataResponse = [] as MediaFile[];
//   for (let i = 0; i < 10000; i++) {
//     fakedDataResponse = fakedDataResponse.concat(JSON.parse(JSON.stringify(fakeMediaFiles)));
//   }

//   const result = fakedDataResponse.map(x => {
//     const mediaFile = new MediaFile();
//     Object.assign(mediaFile, x);
//     return mediaFile;
//   });

//   return result;
// }

const fakeCount = 93827;

export function getMediaFileByCountSync(mediaFileCount: number){
  var mediaFile = new MediaFile();
  mediaFile.filePath = `https://via.placeholder.com/60x60.png?text=test-image-${mediaFileCount}
  `;
  mediaFile.displayCount = mediaFileCount;
  return mediaFile;
}
export async function getMediaFileByCount(mediaFileCount: number): Promise<MediaFile>{
  var mediaFile = new MediaFile();
  mediaFile.filePath = `https://via.placeholder.com/60x60.png?text=test-image-${mediaFileCount}
  `;
  mediaFile.displayCount = mediaFileCount;

  return new Promise<MediaFile>((resolve, reject) => {
    resolve(mediaFile);
  });
}
export async function getTotalFileCount(): Promise<number>{
  return new Promise((resolve, reject) => {
    resolve(fakeCount);
  });
}

export async function getMediaFiles(from: number, to: number): Promise<MediaFile[]>{
  let startCount = from;
  let endCount = to;
  let result = [] as MediaFile[];
  if (startCount > fakeCount){
    return [];
  }
  if (startCount < fakeCount && endCount < fakeCount){
    endCount = fakeCount;
  }

  for (let i = startCount; i< endCount; i++){
    var mediaFile = new MediaFile();
    mediaFile.filePath = `https://via.placeholder.com/60x60.png?text=test-image-${i}
    `;
    mediaFile.displayCount = i;
    result.push(mediaFile);
  }

  return new Promise((resolve, reject)=>{
    setTimeout(()=>{
      resolve(result);
    })
  });
}
