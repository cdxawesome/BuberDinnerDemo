# Create Menu
## Create Menu Request
```
POST /hosts/{hostId}/menus
```

```json
{
    "name": "Menu Name",
    "description": "Menu Description",
    "sections": [
        {
            "name": "Appetizers",
            "description": "Starters",
            "items":[
                {
                    "name": "Fried Pickles",
                    "description": "Deep fried pickles"
                }
            ]
        }
    ]
}
```

## Menu Response
```json
{
    "id":"0000000000-0000000000-000000000",
    "name":"Yummy Menu",
    "description":"Menu Description",
    "averageRating":4.5,
    "sections":[
        {
            "id":"0000000000-0000000000-000000000",
            "name":"Appetizers",
            "description":"Starters",
            "items":[
                {
                    "id":"0000000000-0000000000-000000000",
                    "name":"Fried Pickles",
                    "description":"Deep fried pickles"
                }
            ]
        }
    ],
    "hostId":"0000000000-0000000000-000000000",
    "dinnerIds":[
        "0000000000-0000000000-000000000"
    ],
    "menuReviewIds":[
        "0000000000-0000000000-000000000"
    ],
    "createdDateTime":"2020-01-01T00:00:00Z",
    "updatedDateTime":"2020-01-01T00:00:00Z"

}
```