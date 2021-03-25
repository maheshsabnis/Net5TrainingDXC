 
function getProducts(){

    return new Promise((resolve,reject)=> {
        let xhr = new XMLHttpRequest();
    
        // subscribe to the responses for success and failure
        // success
        xhr.onload = function(){
            // check for Http Status as 200
            if(xhr.status== 200) {
                console.log(`In onload ${xhr.response}`);
                // resolve and notify the response to client / subscriber
                resolve(xhr.response); 
            } else {
                // reject if there is different status code
                reject('Some Error Occured with the status code');
            }
        };
    
        // failure
        xhr.onerror = function(){
             // reject if there is different status code
             reject('Some Error Occured with Http Communication');
        };
    
        // initiate the request
        xhr.open("GET", "https://apiapptrainingnewapp.azurewebsites.net/api/Products");
        // send the request
        xhr.send();

    });
}

 