import { SHORTS_DURATION } from "./constants";

function transformMovies(movies) {
  movies.forEach(movie => {
    if (!movie.image) {
      movie.image = 'https://i.ytimg.com/vi/GHgWdkHseVY/maxresdefault.jpg';
      movie.thumbnail = 'https://i.ytimg.com/vi/GHgWdkHseVY/maxresdefault.jpg';
    } else {
      movie.thumbnail = `https://api.nomoreparties.co${movie.image.formats.thumbnail.url}`
      movie.image = `https://api.nomoreparties.co${movie.image.url}`
    }
    if(!movie.country) {
      movie.country = 'Russia';
    }
    if(!movie.nameEN) {
      movie.nameEN = movie.nameRU;
    }
  });
  return movies
}

function getSavedCard(movie, array) {
  //console.log(movie.id+"")
  return array.find(item => item.movieId == (movie.id || movie.movieId));
}

function changeDuration(duration) {
  const hours = Math.trunc(duration / 60);
  const minutes = duration % 60;
  return hours === 0 ? `${minutes}м` : `${hours}ч ${minutes}м`;
}


function filterShortMovies(movies) {
  return movies.filter(movie => movie.duration < SHORTS_DURATION);
}

function findMoviesByQuery(movies, userQuery, shortMoviesCheckbox) {
  const moviesByUserQuery = movies.filter((movie) => {
    const movieRu = String(movie.nameRU).trim().toLowerCase();
    const movieEn = String(movie.nameEN).trim().toLowerCase();
    const userMovie = userQuery.trim().toLowerCase();
    return movieRu.includes(userMovie) || movieEn.includes(userMovie);
  });

  return shortMoviesCheckbox === true ?
    filterShortMovies(moviesByUserQuery) : moviesByUserQuery;
}

export {
  getSavedCard,
  changeDuration,
  filterShortMovies,
  findMoviesByQuery,
  transformMovies
};