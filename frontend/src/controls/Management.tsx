import { Modal } from "bootstrap";
import { getAllMediaDirectories } from "../services/mediaDirectoryService";

export class Management {
    el: HTMLElement;
    addMediaDirectoryDialog: AddMediaDirectoryDialog;
    constructor(private parenetElement: HTMLElement) {
        this.render();
    }

    async renderDirectories() {
        const mediaDirectories = await getAllMediaDirectories();

        const result = <div class="management-directories">
            <h2>目录管理</h2>
            {
                mediaDirectories.map(x => {
                    return <div class="row management-directories-directory">
                        <div class="col-md-2 fw-bold">
                            路径
                        </div>
                        <div class="col-md-10">
                            {x.path}
                        </div>
                        <div class="col-md-2 fw-bold">
                            文件列表
                        </div>
                        <div class="col-md-10">
                            ---grid in here ---
                            ---for path {x.path} ---
                        </div>
                    </div>
                })
            }

            <button class="btn btn-primary" onClick={this.openAddDirectoryDialog}>添加目录</button>
        </div>;
        return result;
    }
    async render() {
        this.el = <div class="container">
            <div class="management">
                {await this.renderDirectories()}
                <hr></hr>
                <div class="management-scanning">
                    <h2>扫描状态</h2>
                    <div class="col-md-3 fw-bold">
                        最近扫描的10个文件
                    </div>
                    <div class="col-md-10">
                        <div>C:\photos\long-path test</div>
                        <div>C:\photos\long-path test</div>
                        <div>C:\photos\long-path test</div>
                        <div>C:\photos\long-path test</div>
                        <div>C:\photos\long-path test</div>
                        <div>C:\photos\long-path test</div>
                        <div>C:\photos\long-path test</div>
                        <div>C:\photos\long-path test</div>
                        <div>C:\photos\long-path test</div>
                        <div>C:\photos\long-path test</div>
                    </div>
                </div>
                <hr></hr>

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
    promise: Promise<AddMediaDirectoryDialogResult>;
    promiseResolve: (value: AddMediaDirectoryDialogResult) => void;
    promiseReject: (reason?: any) => void;
    pathInputEl: HTMLElement;
    constructor() {
        const addDirectoryDialog = new Modal(
            <div class="modal" tabIndex={-1}>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Add Media Directory</h5>
                            <button type="button" class="btn-close" onClick={this.hide} aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="mb-3">
                                <label for="exampleFormControlInput1" class="form-label">Path</label>
                                {this.pathInputEl = <input type="text" class="form-control" id="directory-path" placeholder="Media Directory Path" />}
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" onClick={this.hide}>Close</button>
                            <button type="button" class="btn btn-primary" onClick={this.onAddMediaDirectoryButtonClicked}>Add Media Directory</button>
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

    async show() {
        this.modal.show();
        this.promise = new Promise((resolve, reject) => {
            this.promiseResolve = resolve;
            this.promiseReject = reject;
        });
        return this.promise;
    }

    async hide() {
        this.promiseResolve({
            hasNewDirectoryAdded: false
        });
        this.modal.hide();
    }

    private onAddMediaDirectoryButtonClicked() {

        this.promiseResolve({
            hasNewDirectoryAdded: true
        });
    }
}