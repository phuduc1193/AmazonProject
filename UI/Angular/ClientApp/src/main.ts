import { enableProdMode } from "@angular/core";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";

import { AppModule } from "./app/app.module";
import { environment } from "./environments/environment";

export function getBaseUrl() {
  return document.getElementsByTagName("base")[0].href;
}

import $ from "jquery";
import "popper.js";
import "bootstrap";

const providers = [
  { provide: "BASE_URL", useFactory: getBaseUrl, deps: [] },
  {
    provide: "APP_NAME",
    useFactory: () => {
      return environment.appName;
    },
    deps: []
  }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers)
  .bootstrapModule(AppModule)
  .catch(err => console.log(err));
