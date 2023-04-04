import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { Pitch } from 'src/models/Pitch';
import { PitchesService } from 'src/services/PitchesService';
import { TranslocoModule } from '@ngneat/transloco';
import { NgIf, AsyncPipe, DatePipe } from '@angular/common';

@Component({
    selector: 'app-pitch-page',
    templateUrl: './pitch-page.component.html',
    styleUrls: ['./pitch-page.component.scss'],
    standalone: true,
    imports: [NgIf, TranslocoModule, RouterLink, AsyncPipe, DatePipe]
})
export class PitchPageComponent implements OnInit {
  constructor(
    private _route: ActivatedRoute,
    private _pitchesService: PitchesService
  ) {}

  public pitch?: Observable<Pitch>;

  ngOnInit(): void {
    const pitchId = this._route.snapshot.params['id'];
    this.pitch = this._pitchesService.getPitch(pitchId);
  }
}
