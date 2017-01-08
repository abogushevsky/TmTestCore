var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { inject } from "aurelia-framework";
import { HttpClient } from "aurelia-fetch-client";
let Values = class Values {
    constructor(http) {
        this.http = http;
        this.values = [];
    }
    activate() {
        return this.http.fetch("http://localhost:YOURPORTNUMBER/api/values").
            then(response => response.json()).then(data => {
            this.values = data;
        });
    }
};
Values = __decorate([
    inject(HttpClient)
], Values);
export { Values };
