import { inject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";

@inject(HttpClient)
export class Values {
    values: string[] = [];

    constructor(private http: HttpClient) { }

    activate() {
        return this.http.fetch("http://localhost:49877/api/values").
            then(response => response.json()).then(data => {
                let arr = new Array<string>();
                arr.push("first");
                arr.push("second");
                this.values = arr;
            });
    }
}