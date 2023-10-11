//External Modules
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { CommonModule } from '@angular/common';
import { MsalModule, MsalRedirectComponent, MsalGuard, MsalInterceptor } from '@azure/msal-angular';
import { InteractionType } from '@azure/msal-browser';
import { PublicClientApplication } from '@azure/msal-browser';
import { MatMenu } from '@angular/material/menu';
//COMPONENTS
import { NavbarComponent } from './navbar/navbar.component';
import { CustomersComponent } from './customers/customers.component';
import { MoviesComponent } from './movies/movies.component';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';
import { CreateMovieComponent } from './create-movie/create-movie.component';
import { AuthComponent } from './auth/auth.component';
import { MovieListPopupComponent } from './movie-list-popup/movie-list-popup.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    CustomersComponent,
    MoviesComponent,
    EditCustomerComponent,
    CreateMovieComponent,
    AuthComponent,
    MovieListPopupComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatSlideToggleModule,
    MatSidenavModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatCardModule,
    MatGridListModule,
    MatMenuModule,
    FlexLayoutModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatCardModule,
    MatDialogModule,
    MatIconModule,
    MsalModule.forRoot(
      new PublicClientApplication({
        auth: {
          clientId: '4e1ff54b-bf34-4f45-83ce-e50fc32967cd',
          authority: 'https://login.microsoftonline.com/common',
          redirectUri: 'http://localhost:4200',
        },
        cache: {
          cacheLocation: "localStorage",
        },
      }), null!,
      {
        interactionType: InteractionType.Popup,
        protectedResourceMap: new Map([
          ["https://localhost:7014/api/", [
            'api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.read',
            'api://dbf7f51e-d046-435b-88ee-c4f9ee872967/to-do-lists.write'
          ]],
        ]),
      }
    ),

  ],

  providers: [
    DatePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent, MsalRedirectComponent],
})
export class AppModule { }







