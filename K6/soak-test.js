// JavaScript source code
import http from 'k6/http';
import { check, group, sleep } from 'k6';

export const options = {
    stages: [
        { target: 15, duration: '30s' },
        { target: 35, duration: '2m' },
        { target: 50, duration: '5m' },
        { target: 0, duration: '10m' }
    ]
};

export default function() {

    const res = http.get('https://localhost:7199/api/v1/Workouts/cached');

    sleep(1);
    const checkRes = check(res, {
        'Success': (r) => r.status === 200
    })

};