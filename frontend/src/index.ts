import { Timeline } from "./controls/Timeline";

class App{
  constructor(parentDom: HTMLElement){
    const timeline = new Timeline(parentDom);
  }
}

const photoViewerRootEl = document.querySelector("#photo-viewer") as HTMLElement;
document.addEventListener("DOMContentLoaded", () => {
  new App(photoViewerRootEl);
})
