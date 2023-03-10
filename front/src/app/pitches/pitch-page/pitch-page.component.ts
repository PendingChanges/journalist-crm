import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Pitch } from 'src/models/Pitch';
import { PitchesService } from 'src/services/PitchesService';

@Component({
  selector: 'app-pitch-page',
  templateUrl: './pitch-page.component.html',
  styleUrls: ['./pitch-page.component.scss'],
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
