import urlJoin from "url-join";

let baseURL = "/";
if (DEBUG){
    baseURL = BASE_URL || "/"
}

export const BaseURL = baseURL;
export async function get<resT>(url: string ): Promise<resT>{
    url = urlJoin(baseURL, url);
    const response = await fetch(url, {
        mode: "cors",
    });
    const result = await response.json() as resT;

    return result;
}

export async function post<reqT, resT>(url:string, requestData: reqT): Promise<resT>{
    url = urlJoin(baseURL, url);
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        mode: "cors",
        body: requestData? JSON.stringify(requestData) : undefined
    });
    const result = await response.json() as resT;
    return result;
}

export async function put<reqT, resT>(url: string, requestData: reqT): Promise<resT>{
    url = urlJoin(baseURL, url);
    const response = await fetch(url, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        mode: "cors",
        body: requestData? JSON.stringify(requestData) : undefined
    });
    const result = await response.json() as resT;
    return result;
}

export async function _delete<reqT, resT>(url: string, requestData: reqT): Promise<resT>{
    url = urlJoin(baseURL, url);
    const response = await fetch(url, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json"
        },
        mode: "cors",
        body: requestData? JSON.stringify(requestData) : undefined
    });
    const result = await response.json() as resT;
    return result;
}