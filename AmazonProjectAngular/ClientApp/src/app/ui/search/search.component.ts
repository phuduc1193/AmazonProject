import { Component, OnInit } from "@angular/core";
import { SearchService } from "../../core/search.service";

@Component({
  selector: "app-search",
  templateUrl: "./search.component.html",
  styleUrls: ["./search.component.scss"]
})
export class SearchComponent implements OnInit {
  constructor(private searchService: SearchService) {}

  ngOnInit() {}

  search(input: string) {
    this.searchService.search(input);
  }
}
