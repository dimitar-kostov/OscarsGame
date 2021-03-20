<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MovieControl.ascx.cs" Inherits="OscarsGame.MovieControl" %>

<div class="movieItem">
   <div class="title">
        <div class="items">                                      
              <a class="linkTitle" href="<%# BuildUrl(Item.Id) %>" title="<%# Item.Overview %>">
                  <%# Item.Title %> (<%# DisplayYear(Item.ReleaseDate) %>)
              </a> 
              <br />
              <a href="<%# BuildImdbUrl(Item.ImdbId) %>" target="_newtab" title="See the info in IMDB" >
                  <img class="imdb" src="/images/icons/imdb.svg" />
              </a>
                                       
         </div>
     </div >   
     <img id="myImg" class="poster" src="<%# BuildPosterUrl(Item.PosterPath) %>" />
         
     <div id="myModal" class="modal">
         <span class="close" onclick="document.getElementById('myModal').style.display='none'">&times;</span>
         <img class="modal-content"  >        
      </div>
</div>
                

