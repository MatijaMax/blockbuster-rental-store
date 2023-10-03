import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'movie-store-webapp';

  ngOnInit() {
    // Function to set the body's min-height based on content and viewport
    function setBodyMinHeight() {
      const body = document.body;
      const contentHeight = document.documentElement.scrollHeight;
      const viewportHeight = window.innerHeight;

      // Set min-height to fill viewport or content height, whichever is greater
      body.style.minHeight = Math.max(viewportHeight, contentHeight) + 'px';
    }

    // Listen for window resize events and recompute min-height
    window.addEventListener('resize', setBodyMinHeight);

    // Initial call to set the min-height when the page loads
    window.addEventListener('load', setBodyMinHeight);
  }
}

