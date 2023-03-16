import * as a  from "tsx-dom";
import { getMediaFileByCountSync, getMediaFiles, getTotalFileCount, initializeTotalCount } from "../services/MediaFileService";
import { MediaFile } from "../services/models/MediaFile";
import PhotoSwipe from "photoswipe";
import PhotoSwipeLightbox from "photoswipe/lightbox";
const PhotoSwipeVideoPlugin = require("photoswipe-video-plugin").default ;

declare module "tsx-dom" {
  export interface TsxConfig {
      // Set one of these to false to disable support for them
      svg: false;
      // html: false;
  }
}


export class Timeline {
  el: HTMLElement;
  scrollingEl: HTMLElement;
  mediaFiles: MediaFile[];
  mediaFilePerLine: number = 6;
  totalCount: number = 0;
  displayAreaBegin: number = 0;
  displayAreaEnd: number = 0;
  lightbox: PhotoSwipeLightbox;
  get viewingHeight(){
    return this.el.offsetHeight;
  }
  get viewingWidth(){
    return this.el.offsetWidth;
  }

  get scrollTop(){
    return this.el.scrollTop;
  }

  get imageWidth(){
     return this.viewingWidth / this.mediaFilePerLine;
  }
  get lineHeight(){
    if (this.mediaFilePerLine == 0){
      return 0;
    }else{
      return this.imageWidth;
    }
  }

  get totalLineCount(){
    const result = Math.ceil(this.totalCount / this.mediaFilePerLine);
    return result;
  }

  get timelineHeight(){
    return this.totalLineCount * this.lineHeight;
  }

  public bufferLineCount: number = 0;

  public currentViewing: MediaFileCountFromTo;


  constructor(parent: HTMLElement) {
    this.el = <div class="photo-viewer__timeline">
      {this.scrollingEl = <div class="photo-viewer__timeline__scrolling"></div>}

    </div>;
    parent.appendChild(this.el);
    this.mediaFiles = [];
    this.initTimeline();

  }
  async initTimeline() {
    this.totalCount = await getTotalFileCount();
    await initializeTotalCount();
    this.scrollingEl.style.height = `${this.timelineHeight}px`;

    setTimeout(() => {
      this.el.scrollTop= this.el.scrollHeight;
      this.render();
      this.bindEvent();
      this.lightbox = new PhotoSwipeLightbox({
        showHideAnimationType: "fade",
        pswpModule: PhotoSwipe
      });
      const videoPlugin = new PhotoSwipeVideoPlugin(this.lightbox)
      this.lightbox.addFilter("numItems", () => {
        return this.totalCount
      });
      this.lightbox.addFilter('itemData', (itemData, index) => {
        let mediaFile = getMediaFileByCountSync(index);
        return {
          src: mediaFile.requestPath,
          // videoSrc: "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4",
          // type: "video"
        };;
      });
      this.lightbox.init();

    });
  }

  
  bindEvent(){
    this.el.addEventListener("scroll", (e: Event) => {
      this.render();
    });
    this.el.addEventListener("click", (e: Event) => {
      let clickedEl = e.target as HTMLElement;
      let timelineCell = clickedEl.closest(".timeline__cell");
      let mediaFileNumber = Number(timelineCell.getAttribute("data-media-file-number"));
      this.lightbox.loadAndOpen(mediaFileNumber);
      
    });
  }

  render(){
    // console.log(`from: ${fromMediaFileNumber}, to: ${toMediaFileNumber}, total: ${toMediaFileNumber - fromMediaFileNumber}`)


    const viewportStartX = this.scrollTop;
      const viewportEndX = this.scrollTop + this.viewingHeight;
      let startLineNumber = Math.floor(viewportStartX / this.lineHeight);
      let endLineNumber = Math.ceil(viewportEndX / this.lineHeight);

      startLineNumber = startLineNumber - this.bufferLineCount;
      endLineNumber = endLineNumber + this.bufferLineCount;

      startLineNumber = Math.max(startLineNumber, 0);
      endLineNumber = Math.min(endLineNumber, this.totalLineCount);

      let fromMediaFileNumber = startLineNumber * this.mediaFilePerLine;
      let toMediaFileNumber = endLineNumber * this.mediaFilePerLine;
      
      toMediaFileNumber = Math.min(toMediaFileNumber, this.totalCount);


    this.renderPhoto(fromMediaFileNumber, toMediaFileNumber)
  }

  async getCurrentViewingFiles(){
    const result = {from: 0, to: 0};
    const scrollTop = this.el.scrollTop;
    
  }

  getRenderingDiff(source: MediaFileCountFromTo, target: MediaFileCountFromTo): RenderDiffResult{
    const expandedTarget = this.expandToMediaNumbers(target);
    if (!source){
      return {
        filesToAdd: expandedTarget,
        filesToRemove: []
      };
    }

    const expandedSource = this.expandToMediaNumbers(source);

    const filesToAdd = expandedTarget.filter(x => !expandedSource.includes(x));
    const filesToRemove = expandedSource.filter(x => !expandedTarget.includes(x));

    return {
      filesToAdd,
      filesToRemove
    }
  }

  expandToMediaNumbers(fromTo: MediaFileCountFromTo){
    let result = [];

    for (let i = fromTo.from; i < fromTo.to; i++){
      result.push(i);
    }

    return result;
  }

  async renderPhoto(from: number, to: number){
    const files = await getMediaFiles(from, to);
    const filesDic = files.reduce<Record<number, MediaFile>>((acc: Record<number, MediaFile>, currentValue) => {
      acc[currentValue.displayCount] = currentValue;
      return acc;
    }, {});


    const diff = this.getRenderingDiff(this.currentViewing, {from, to});
    this.renderMediaFiles(diff.filesToAdd, filesDic);
    this.removeMediaFiles(diff.filesToRemove);
    this.currentViewing = {
      from, to
    }
  }

  renderMediaFiles(filesToAdd: number[], filesDic: Record<number, MediaFile>){
    for (let i = 0; i < filesToAdd.length; i++){
      let mediaFileNumber = filesToAdd[i];
      let cellPosition = this.getCellRenderPosition(mediaFileNumber);
      let fileToRender = filesDic[mediaFileNumber];

      // defence coding -- slow network. 
      let imageAlreadyRendered = !!document.querySelector(`[data-media-file-number='${mediaFileNumber}']`)
      if (imageAlreadyRendered){
        return;
      }

      let imageEl = <div class="timeline__cell" 
            style={`left: ${cellPosition.x}px; top: ${cellPosition.y}px; width: ${this.imageWidth}px; height: ${this.lineHeight}px`} 
            data-line-number={cellPosition.line} 
            data-column-number={cellPosition.column}
            data-media-file-number={mediaFileNumber}>
              <img class="timeline__cell__img" src={fileToRender.thumbnailFilePath || fileToRender.requestPath}></img>
      </div>;
      this.el.appendChild(imageEl)

    }
  }
  removeMediaFiles(filesToRemove: number[]){
    filesToRemove.forEach((f) => {
      let cellEl = this.el.querySelector(`[data-media-file-number='${f}']`);
      let imageEl = cellEl.querySelector(".timeline__cell__img") as HTMLImageElement;
      imageEl.src = "";
      if (cellEl){
        this.el.removeChild(cellEl);
      }
    })

  }

  getCellRenderPosition(mediaFileNumber: number){
    const {line, column} = this.getCellLineColumnPosition(mediaFileNumber);
    const y = line * this.lineHeight;
    const x = column * this.imageWidth;
    return {
      x,
      y,
      line,
      column
    }

  }
  getCellLineColumnPosition(mediaFileNumber: number){
    const line = Math.floor(mediaFileNumber / this.mediaFilePerLine) //zero based line number;
    const column = (mediaFileNumber % this.mediaFilePerLine)// zero based column number ;
    return {
      line,
      column
    }
  }
}

declare type MediaFileCountFromTo = {
  readonly from: number;
  readonly to: number;
}

declare type RenderDiffResult = {
  readonly filesToAdd: number[];
  readonly filesToRemove: number[];
}