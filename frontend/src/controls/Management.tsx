import { Modal } from "bootstrap";
import { getAllMediaDirectories, startScanDirectory } from "../services/mediaDirectoryService";

export class Management {
    el: HTMLElement;
    addMediaDirectoryDialog: AddMediaDirectoryDialog;
    constructor(private parenetElement: HTMLElement) {
        this.render();
    }

    async renderDirectories() {
        const mediaDirectories = await getAllMediaDirectories();

        const result = <div class="management-directories">
            <h2>Directory Management</h2>
            {
                mediaDirectories.map(x => {
                    return <div class="card">
                        <div class="card-body">

                            <div class="row management-directories-directory">
                                <div class="col-md-2 fw-bold">
                                    Path
                                </div>
                                <div class="col-md-10">
                                    {x.path}
                                </div>
                                <div class="col-md-2 fw-bold">
                                    Files Count
                                </div>
                                <div class="col-md-10">
                                    {x.photosCount}
                                </div>
                                <div class="col-md-5">
                                <button class="btn btn-secondary" onClick={() => {this.scanDirectory(x.id)}}>Scan Directory</button>
                                </div>
                            </div>
                        </div>
                    </div>
                })
            }

            <hr></hr>
            <button class="btn btn-primary" onClick={this.openAddDirectoryDialog.bind(this)}>Add Directory</button>
        </div>;
        return result;
    }
    async scanDirectory(directoryId: string) {
        await startScanDirectory(directoryId);
        alert("Scan Directory Job Triggered. ")
    }
    async render() {
        this.el = <div class="container">
            <div class="management">
                {await this.renderDirectories()}

            </div>
        </div>
        this.parenetElement.appendChild(this.el);
    }

    openAddDirectoryDialog() {
        const dialog = new AddMediaDirectoryDialog();
        this.addMediaDirectoryDialog = dialog;
        this.addMediaDirectoryDialog.show();
    }
}

type AddMediaDirectoryDialogResult = {
    hasNewDirectoryAdded: boolean
}
class AddMediaDirectoryDialog {
    modal: Modal;
    pathInputEl: HTMLElement;
    constructor() {
        const addDirectoryDialog = new Modal(
            <div class="modal" tabIndex={-1}>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Media Directory</h5>
                            <button type="button" class="btn-close" onClick={this.hide.bind(this)} aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="mb-3">
                                <label for="exampleFormControlInput1" class="form-label">Path</label>
                                {this.pathInputEl = <input type="text" class="form-control" id="directory-path" placeholder="Media Directory Path" />}
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" onClick={this.hide.bind(this)}>Close</button>
                            <button type="button" class="btn btn-primary" onClick={this.onAddMediaDirectoryButtonClicked.bind(this)}>Add Media Directory</button>
                        </div>
                    </div>
                </div>
            </div>,
            {
                focus: true
            }
        )
        this.modal = addDirectoryDialog;
    }

    show() {
        this.modal.show();
    }

    hide() {
        this.modal.hide();
    }

    private onAddMediaDirectoryButtonClicked() {
        
    }
}