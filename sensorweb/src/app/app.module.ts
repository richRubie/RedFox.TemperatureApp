import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppComponent } from './app.component';
import { DataService } from './data.service';
import { ChartsModule } from 'ng2-charts/ng2-charts';
import { SecurityService } from './security.service';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        ChartsModule
    ],
    providers: [DataService, SecurityService],
    bootstrap: [AppComponent]
})
export class AppModule { }
